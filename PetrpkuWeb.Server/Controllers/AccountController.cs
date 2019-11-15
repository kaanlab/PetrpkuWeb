using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        public async Task<ActionResult<LoginResult>> Login(LoginViewModel model)
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

            var idettityUser = await _db.Users.Include(u => u.AssosiatedUser).SingleOrDefaultAsync(u => u.UserName == model.Username);
            if (idettityUser is null || !idettityUser.AssosiatedUser.IsActive)
            {
                return BadRequest(new LoginResult { Successful = false, Error = "User is locked" });
            }

            // update email if changed
            if (appUserIdentity.Email != ldapUser.Email)
            {
                appUserIdentity.Email = ldapUser.Email;
                appUserIdentity.NormalizedEmail = ldapUser.Email.ToUpperInvariant();

                await _userManager.UpdateAsync(appUserIdentity);
            }

            // update groups if changed
            //await RegisterRoles(appUserIdentity, ldapUser);

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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpireInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [Authorize(Roles = AuthRole.ADMIN)]
        [HttpGet("search/{authUserName}")]
        public ActionResult<IAuthUser> SearchUser(string authUserName)
        {
            var ldapUser = _appAuthenticationService.Search(authUserName);
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
        public async Task<ActionResult> AddAccount(IAuthUser authUser)
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
            return await _db.Users
                .Include(u => u.AssosiatedUser)
                .OrderBy(u => u.AppUserId)
                .ToListAsync();
        }

        //[Authorize(Roles = AuthRole.ADMIN)]
        //[HttpPut("identity/update/{userName}")]
        //public async Task<ActionResult> DisabledAuthUser(string userName, AppUserIdentity appUserIdentity)
        //{
        //    if (userName == appUserIdentity.UserName)
        //    {
        //        _db.Users.Update(appUserIdentity);
        //        await _db.SaveChangesAsync();
        //        return Ok(appUserIdentity);
        //    }
        //    return BadRequest();

        //   // if (ModelState.IsValid)
        //   // {
        //       // var user = await _db.Users.SingleOrDefaultAsync(u => u.UserName == userName);
        //       // if (user is null)
        //       // {
        //       //     return NotFound();
        //        //}

        //       // var appUser = await _db.AppUsers.SingleOrDefaultAsync(u => u.AppUserId == user.AppUserId);
        //        //appUser.DisplayName += $"( удален {DateTime.Now.ToShortDateString()})";
        //        //appUser.IsActive = false;

        //        //_db.Users.Update(user);
        //       // await _db.SaveChangesAsync();
        //       // return Ok(appUser);
        //    //}

        //   // return BadRequest(ModelState);
        //}


        private Task<AppUserIdentity> AddIdentityUser(IAuthUser authUser)
        {
            return Task.Run(() =>
            {
                var appUserIdentity = new AppUserIdentity()
                {
                    UserName = authUser.UserName,
                    NormalizedUserName = authUser.UserName.ToUpperInvariant(),
                    Email = authUser.Email,
                    NormalizedEmail = authUser.Email.ToUpperInvariant(),
                    DisplayName = authUser.DisplayName,
                    AssosiatedUser = new AppUser()
                    {
                        DisplayName = authUser.DisplayName,
                        IsActive = true,
                        IsDuty = false
                    }
                };

                return appUserIdentity;
            });
        }

        //private async Task RegisterRoles(AppUserIdentity appUserIdentity, IAuthUser authUser)
        //{
        //    var roles = await _userManager.GetRolesAsync(appUserIdentity);
        //    if (roles is { })
        //    {
        //        await _userManager.RemoveFromRolesAsync(appUserIdentity, roles);
        //    }
        //    await _userManager.AddToRolesAsync(appUserIdentity, authUser.Roles);
        //}
    }
}