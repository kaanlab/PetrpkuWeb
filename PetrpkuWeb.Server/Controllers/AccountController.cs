using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetrpkuWeb.NovellDirectoryLdap;
using PetrpkuWeb.Server.Data;
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

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return Unauthorized(new { message = "Username or password can't be null" });

            var ldapUser = _appAuthenticationService.Login(model.Username, model.Password);
            if (ldapUser == null)
            {
                return BadRequest(new LoginResult { Successful = false, Error = "Bad username or password" });
            }

            var appUserIdentity = await _userManager.FindByNameAsync(ldapUser.UserName);
            if (appUserIdentity == null)
            {
                appUserIdentity = AddIdentityUser(ldapUser);
                await _userManager.CreateAsync(appUserIdentity);
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

        [HttpGet("search/{ldapUserName}")]
        public ActionResult<IAuthUser> SearchUser(string ldapUserName)
        {
            var ldapUser = _appAuthenticationService.Search(ldapUserName);
            if (ldapUser == null)
            {
                return BadRequest(new LoginResult { Successful = false, Error = "Bad username" });
            }
            return Ok(ldapUser);
        }

        [HttpGet("ldap/all")]
        public ActionResult<List<LdapUser>> GetAll()
        {
            var ldapUser = _appAuthenticationService.SearchAll();
            if (ldapUser.Count > 0)
            {
                return Ok(ldapUser);
            }
            return BadRequest(new { Message = "Пользователи отсутствуют" });
        }

        [HttpPost("identity/add")]
        public async Task<ActionResult<bool>> AddAccount(LdapUser authUser)
        {
            if (authUser != null)
            {
                var appUserIdentity = AddIdentityUser(authUser);

                var result = await _userManager.CreateAsync(appUserIdentity);

                if (result.Succeeded)
                    return Ok();

                return BadRequest();
            }

            return BadRequest();
        }

        [HttpGet("identity/all")]
        public async Task<ActionResult<List<AppUserIdentity>>> GetAllAuthUsers()
        {
            return await _db.Users.ToListAsync();
        }

        [HttpDelete("identity/delete/{appUserIdentityId}")]
        public IActionResult Delete(string appUserIdentityId)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.Find(appUserIdentityId);
                if (user == null)
                {
                    return NotFound();
                }

                var appUser = _db.AppUsers.Find(user.AssosiateUser.AppUserId);
                appUser.DisplayName = user.DisplayName + "(d)";
                appUser.IsActive = false;

                _db.Users.Remove(user);
                _db.SaveChanges();
                return NoContent();
            }

            return BadRequest(ModelState);
        }


        private AppUserIdentity AddIdentityUser(IAuthUser authUser)
        {
            var appUserIdentity = new AppUserIdentity()
            {
                UserName = authUser.UserName,
                DisplayName = authUser.DisplayName,
                AssosiateUser = new AppUser()
                {
                    DisplayName = authUser.DisplayName,
                    PhotoUrl = @"/img/user/default_avatar.png",
                    IsActive = true,
                    IsDuty = false
                }
            };

            return appUserIdentity;
        }
    }
}