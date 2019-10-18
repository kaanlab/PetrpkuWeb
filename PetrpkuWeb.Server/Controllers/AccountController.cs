using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetrpkuWeb.NovellDirectoryLdap;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Shared.Models;
using PetrpkuWeb.Shared.ViewModels;

namespace PetrpkuWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAppAuthenticationService _appAuthenticationService;
        private readonly SignInManager<AppUserIdentity> _signInManager;
        private readonly UserManager<AppUserIdentity> _userManager;
        private readonly AppDbContext _db;

        public AccountController(
            IConfiguration configuration,
            SignInManager<AppUserIdentity> signInManager,
            IAppAuthenticationService appAuthenticationService,
            UserManager<AppUserIdentity> userManager,
            AppDbContext db)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _appAuthenticationService = appAuthenticationService;
            _userManager = userManager;
            _db = db;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return Unauthorized(new { message = "Username or password can't be null" });

            var ldapUser = _appAuthenticationService.Login(model.Username, model.Password);
            if (ldapUser is null)
            {
                return BadRequest(new LoginResult { Successful = false, Error = "Bad username or password" });
            }

            var appUserIdentity = await _userManager.FindByNameAsync(ldapUser.UserName);
            if (appUserIdentity is null)
            {
#if DEBUG
                appUserIdentity = await AddIdentityUser(ldapUser);
                await _userManager.CreateAsync(appUserIdentity);
#else
                return BadRequest(new LoginResult { Successful = false, Error = $"Can't find user {ldapUser.UserName} in AD" });
#endif
            }

            if (appUserIdentity.Email != ldapUser.Email)
            {
                appUserIdentity.Email = ldapUser.Email;
                appUserIdentity.NormalizedEmail = ldapUser.Email.ToUpperInvariant();

                await _userManager.UpdateAsync(appUserIdentity);
            }

            await _signInManager.SignInAsync(appUserIdentity, model.RememberMe);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.WindowsAccountName, appUserIdentity.DisplayName),
                new Claim(ClaimTypes.Name, appUserIdentity.UserName),
                new Claim(ClaimTypes.UserData, appUserIdentity.AppUserId.ToString())
            };

            foreach (var role in ldapUser.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration.GetSection("Jwt")["ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration.GetSection("Jwt")["Issuer"],
                _configuration.GetSection("Jwt")["Audience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [Authorize(Roles = AuthRole.ADMIN)]
        [HttpGet("search/{ldapUserName}")]
        public ActionResult<IAuthUser> SearchUser(string ldapUserName)
        {
            var ldapUser = _appAuthenticationService.Search(ldapUserName);
            if (ldapUser is null)
            {
                return BadRequest(new LoginResult { Successful = false, Error = "Bad username" });
            }
            return Ok(ldapUser);
        }

        [Authorize(Roles = AuthRole.ADMIN)]
        [HttpGet("ldap/all")]
        public async Task<ActionResult<List<LdapUser>>> GetAll()
        {
            var ldapUsers = _appAuthenticationService.SearchAll();
            if (ldapUsers.Count > 0)
            {
                var authUsers = await _db.Users.ToListAsync();
                ldapUsers.RemoveAll(r => authUsers.Exists(u => u.UserName == r.UserName));

                return Ok(ldapUsers);
            }
            return BadRequest(new { Message = "Пользователи отсутствуют" });
        }

        [Authorize(Roles = AuthRole.ADMIN)]
        [HttpPost("identity/add")]
        public async Task<ActionResult> AddAccount(LdapUser authUser)
        {
            if (authUser is { })
            {
                var appUserIdentity = await AddIdentityUser(authUser);
                var result = await _userManager.CreateAsync(appUserIdentity);

                if (result.Succeeded)
                {
                    return Ok(appUserIdentity);
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [Authorize(Roles = AuthRole.ADMIN)]
        [HttpGet("identity/all")]
        public async Task<ActionResult<List<AppUserIdentity>>> GetAllAuthUsers()
        {
            return await _db.Users.ToListAsync();
        }

        [Authorize(Roles = AuthRole.ADMIN)]
        [HttpDelete("identity/delete/{userName}")]
        public async Task<ActionResult> Delete(string userName)
        {
            if (ModelState.IsValid)
            {
                var user = await _db.Users.SingleOrDefaultAsync(u => u.UserName == userName);
                if (user is null)
                {
                    return NotFound();
                }

                var appUser = await _db.AppUsers.SingleOrDefaultAsync(u => u.AppUserId == user.AppUserId);
                appUser.DisplayName += $"( удален {DateTime.Now.ToShortDateString()})";
                appUser.IsActive = false;

                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return Ok(appUser);
            }

            return BadRequest(ModelState);
        }


        private async Task<AppUserIdentity> AddIdentityUser(IAuthUser authUser)
        {
            var avatar = await _db.Attachments.SingleOrDefaultAsync(a => a.Path == @"/img/user/default_avatar.png");
            var building = await _db.Buildings.SingleOrDefaultAsync(b => b.Name == "пусто");
            var department = await _db.Departments.SingleOrDefaultAsync(d => d.Name == "пусто");

            var appUserIdentity = new AppUserIdentity()
            {
                UserName = authUser.UserName,
                NormalizedUserName = authUser.UserName.ToUpperInvariant(),
                Email = authUser.Email,
                NormalizedEmail = authUser.Email.ToUpperInvariant(),
                DisplayName = authUser.DisplayName,
                AssosiateUser = new AppUser()
                {
                    DisplayName = authUser.DisplayName,
                    Avatar = avatar,
                    Building = building,
                    Department = department,
                    IsActive = true,
                    IsDuty = false
                }
            };

            return appUserIdentity;
        }
    }
}