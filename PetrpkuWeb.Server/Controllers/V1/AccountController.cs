using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetrpkuWeb.NovellDirectoryLdap;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Server.Services;
using PetrpkuWeb.Shared.Contracts.V1;
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Shared.ViewModels;

namespace PetrpkuWeb.Server.Controllers.V1
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILdapAuthenticationService _ldapAuthenticationService;
        private readonly SignInManager<AppUserIdentity> _signInManager;
        private readonly IAppIdentityService _appIdentityService;
        private readonly IMapper _mapper;

        public AccountController(
            IConfiguration configuration,
            SignInManager<AppUserIdentity> signInManager,
            ILdapAuthenticationService ldapAuthenticationService,
            IAppIdentityService appIdentityService,
            IMapper mapper)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _ldapAuthenticationService = ldapAuthenticationService;
            _appIdentityService = appIdentityService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Account.LOGIN)]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return Unauthorized(new { message = "Username or password can't be null" });

            var appUserIdentity = await _signInManager.UserManager.FindByNameAsync(model.Username);
            if (appUserIdentity is null || !appUserIdentity.IsActive)
            {
                //appUserIdentity = await _appIdentityService.AddIdentityUser(ldapUser);
                //await _userManager.CreateAsync(appUserIdentity);
                return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });
            }

            if (appUserIdentity.IsLdapUser)
            {
                var ldapUser = _ldapAuthenticationService.Login(model.Username, model.Password);
                if (ldapUser is null)
                {
                    return BadRequest(new LoginResult { Successful = false, Error = "Bad username or password" });
                }

                await _appIdentityService.UpdateEmail(appUserIdentity, ldapUser);

                await _signInManager.SignInAsync(appUserIdentity, model.RememberMe);
            }
            else 
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });
            }

            var appUserIdentityRoles = await _signInManager.UserManager.GetRolesAsync(appUserIdentity);

            var token = CreateJwtToken(appUserIdentity, appUserIdentityRoles);

            return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [Authorize(Roles = AuthRoles.ADMIN)]
        [HttpGet(ApiRoutes.Account.SEARCH + "/{authUserName}")]
        public ActionResult SearchAuthUser(string authUserName)
        {
            var ldapUser = _ldapAuthenticationService.Search(authUserName);
            if (ldapUser is null)
            {
                return BadRequest(new LoginResult { Successful = false, Error = "Bad username" });
            }
            return Ok(ldapUser);
        }

        [Authorize(Roles = AuthRoles.ADMIN)]
        [HttpGet(ApiRoutes.Account.GETALL_LDAPUSERS)]
        public async Task<ActionResult> GetAllLdapUsers()
        {
            var ldapUsers = _ldapAuthenticationService.SearchAll();
            if (ldapUsers.Count > 0)
            {
                var authUsers = await _appIdentityService.GetAllIdentityUsers();
                ldapUsers.RemoveAll(r => authUsers.Exists(u => u.UserName == r.UserName));

                return Ok(ldapUsers);
            }
            return BadRequest(new { Message = "Пользователи отсутствуют" });
        }

        [Authorize(Roles = AuthRoles.ADMIN)]
        [HttpPost(ApiRoutes.Account.ADD_IDENTITY)]
        public async Task<ActionResult> AddAuthUser(IAuthUser authUser)
        {
            if (authUser is { })
            {
                var appUserIdentity = await _appIdentityService.AddIdentityUser(authUser);

                return Ok(_mapper.Map<AppUserIdentityViewModel>(appUserIdentity));
            }
            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN)]
        [HttpGet(ApiRoutes.Account.GETALL_IDENTITIES)]
        public async Task<ActionResult> GetAllAuthUsers()
        {
            var identityUsers = await _appIdentityService.GetAllIdentityUsersOrderById();

            return Ok(_mapper.Map<AppUserIdentityViewModel>(identityUsers));
        }


        private JwtSecurityToken CreateJwtToken(AppUserIdentity appUserIdentity, IList<string> appUserIdentityRoles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.WindowsAccountName, appUserIdentity.DisplayName),
                new Claim(ClaimTypes.Name, appUserIdentity.UserName),
                new Claim(ClaimTypes.UserData, appUserIdentity.AppUserId.ToString())
            };

            foreach (var role in appUserIdentityRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpireInDays"]));

            return new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );
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


        //private Task<AppUserIdentity> AddIdentityUser(IAuthUser authUser)
        //{
        //    return Task.Run(() =>
        //    {
        //        var appUserIdentity = new AppUserIdentity()
        //        {
        //            UserName = authUser.UserName,
        //            NormalizedUserName = authUser.UserName.ToUpperInvariant(),
        //            Email = authUser.Email,
        //            NormalizedEmail = authUser.Email.ToUpperInvariant(),
        //            DisplayName = authUser.DisplayName,
        //            AssosiatedUser = new AppUser()
        //            {
        //                DisplayName = authUser.DisplayName,
        //                IsActive = true,
        //                IsDuty = false
        //            }
        //        };

        //        return appUserIdentity;
        //    });
        //}

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