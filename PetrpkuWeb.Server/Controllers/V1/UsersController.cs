using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Server.Services;
using PetrpkuWeb.Shared.Contracts.V1;
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Shared.ViewModels;

namespace PetrpkuWeb.Server.Controllers.V1
{
    //[Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IAppUsersService _appUsersService;
        private readonly IMapper _mapper;

        public UsersController(
            IAppUsersService appUsersService, 
            IMapper mapper)
        {
            _appUsersService = appUsersService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Users.ALL_ACTIVE)]
        public async Task<ActionResult> GetAllActive()
        {
            var appUsers = await _appUsersService.GetAllActiveOrderByDispalyName();

            return Ok(_mapper.Map<List<AppUserViewModel>>(appUsers));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpGet(ApiRoutes.Users.ALL_ACTIVE_DUTIES)]
        public async Task<ActionResult> GetAllActiveDuties()
        {
            var appUsers =  await _appUsersService.GetAllActiveDutiesOrderByDispalyName();

            return Ok(_mapper.Map<List<AppUserViewModel>>(appUsers));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpGet(ApiRoutes.Users.ALL)]
        public async Task<ActionResult> GetAll()
        {
            var appUsers = await _appUsersService.GetAllOrderByDispalyName();

            return Ok(_mapper.Map<List<AppUserViewModel>>(appUsers));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpGet(ApiRoutes.Users.USER + "/{appUserId}")]
        public async Task<ActionResult> GetUser(string appUserId)
        {
            var appUser = await _appUsersService.GetById(appUserId);

            return Ok(_mapper.Map<AppUserViewModel>(appUser));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Users.BIRTHDAYS)]
        public async Task<ActionResult> GetBirthdays()
        {
            var firstDayOfWeek = DateTime.Now.FirstDayOfWeek();
            // AddDay(1) because a DateTime used as a date is really the very beginning of that day, 
            // and doesn't extend to the end of the day
            var lastDayOfWeek = DateTime.Now.LastDayOfWeek().AddDays(1);

            var appUsers = await _appUsersService.GetBirthdaysForOneWeek(firstDayOfWeek, lastDayOfWeek);

            return Ok(_mapper.Map<List<AppUserViewModel>>(appUsers));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpPut(ApiRoutes.Users.UPDATE + "/{appUserId:int}")]
        public async Task<ActionResult> UpdateUserAsync(int appUserId, AppUserViewModel appUser)
        {
            if (appUserId == appUser.AppUserId)
            {
                var userToUpdate = _mapper.Map<AppUser>(appUser);
                var updated = await _appUsersService.Update(userToUpdate);
                if(updated)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}