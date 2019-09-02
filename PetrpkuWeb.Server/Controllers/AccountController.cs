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
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetrpkuWeb.NovellDirectoryLdap;
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


        public AccountController(
            IConfiguration configuration,
            SignInManager<AppUserIdentity> signInManager,
            IAppAuthenticationService appAuthenticationService,
            UserManager<AppUserIdentity> userManager
        )
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _appAuthenticationService = appAuthenticationService;
            _userManager = userManager;

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return Unauthorized(new {message = "Username or password can't be null"});

            var ldapUser = _appAuthenticationService.Login(model.Username, model.Password);
            if (ldapUser == null)
            {
                return BadRequest(new LoginResult {Successful = false, Error = "Bad username or password"});
            }

            var appUserIdentity = await _userManager.FindByNameAsync(ldapUser.Username);
            if (appUserIdentity == null)
            {
                appUserIdentity = new AppUserIdentity()
                {
                    UserName = ldapUser.Username,
                    DisplayName = ldapUser.DisplayName,
                    AssosiateUser = new AppUser()
                    {
                        DisplayName = ldapUser.DisplayName
                    }
                };

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

            return Ok(new LoginResult {Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token)});
        }


        [HttpGet("search/{userName}")]
        public ActionResult<IAuthUser> SearchAccount(string userName)
        {
            var ldapUser = _appAuthenticationService.Search(userName);
            if (ldapUser == null)
            {
                return BadRequest(new LoginResult { Successful = false, Error = "Bad username" });
            }
            return Ok(ldapUser);
        }

        [HttpGet("searchallfromldap")]
        public ActionResult<List<LdapUser>> SearchAllFromLdap()
        {
            var ldapUser = _appAuthenticationService.SearchAll();
            if (ldapUser.Count > 0)
            {
                return Ok(ldapUser);
            }
            return BadRequest(new { Message = "Пользователи отсутствуют" });
        }
    }
}