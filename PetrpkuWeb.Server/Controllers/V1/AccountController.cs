using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetrpkuWeb.NovellDirectoryLdap;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Server.Services;
using PetrpkuWeb.Shared.Contracts.V1;
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Shared.Models;
using PetrpkuWeb.Shared.ViewModels;

namespace PetrpkuWeb.Server.Controllers.V1
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILdapAuthenticationService _ldapAuthenticationService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUsersService _appUsersService;
        private readonly IMapper _mapper;

        public AccountController(
            IConfiguration configuration,
            SignInManager<AppUser> signInManager,
            ILdapAuthenticationService ldapAuthenticationService,
            IAppUsersService appUsersService,
            IMapper mapper)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _ldapAuthenticationService = ldapAuthenticationService;
            _appUsersService = appUsersService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Account.LOGIN)]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return Unauthorized(new { message = "Username or password can't be null" });

            var appUser = await _signInManager.UserManager.FindByNameAsync(model.Username);
            if (appUser is null || !appUser.IsActive)
            {
                //appUserIdentity = await _appIdentityService.AddIdentityUser(ldapUser);
                //await _userManager.CreateAsync(appUserIdentity);
                return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });
            }

            if (appUser.LdapAuth)
            {
                var ldapUser = _ldapAuthenticationService.Login(model.Username, model.Password);
                if (ldapUser is null)
                {
                    return BadRequest(new LoginResult { Successful = false, Error = "Bad username or password" });
                }

                if (appUser.Email != ldapUser.Email)
                    await _appUsersService.UpdateEmail(appUser, ldapUser);

                await _signInManager.SignInAsync(appUser, model.RememberMe);
            }
            else 
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });
            }

            var appUserRoles = await _signInManager.UserManager.GetRolesAsync(appUser);

            var token = CreateJwtToken(appUser, appUserRoles);

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
        [HttpGet(ApiRoutes.Account.ALL_LDAPUSERS)]
        public async Task<ActionResult> GetAllLdapUsers()
        {
            var ldapUsers = _ldapAuthenticationService.SearchAll();
            if (ldapUsers.Count > 0)
            {
                var authUsers = await _appUsersService.GetAll();
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
                var appUserIdentity = await _appUsersService.Add(authUser);

                return Ok(_mapper.Map<AppUserViewModel>(appUserIdentity));
            }
            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN)]
        [HttpGet(ApiRoutes.Account.ALL_IDENTITIES)]
        public async Task<ActionResult> GetAllAuthUsers()
        {
            var appUsers = await _appUsersService.GetAllOrderById();

            return Ok(_mapper.Map<IEnumerable<AppUserViewModel>>(appUsers));
        }


        private JwtSecurityToken CreateJwtToken(AppUser appUser, IList<string> appUserRoles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.WindowsAccountName, appUser.DisplayName),
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(ClaimTypes.UserData, appUser.Id.ToString())
            };

            foreach (var role in appUserRoles)
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