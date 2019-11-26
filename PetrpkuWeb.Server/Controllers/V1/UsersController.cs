using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet(ApiRoutes.Users.GETALL_ACTIVE)]
        public async Task<ActionResult<List<AppUser>>> GetAllActive()
        {
            return await _appUsersService.GetAllActiveOrderByDispalyName();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpGet(ApiRoutes.Users.GETALL_ACTIVE_DUTIES)]
        public async Task<ActionResult<List<AppUser>>> GetAllActiveDuties()
        {
            return await _appUsersService.GetAllActiveDutiesOrderByDispalyName();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpGet(ApiRoutes.Users.GETALL)]
        public async Task<ActionResult<List<AppUser>>> GetAll()
        {
            return await _appUsersService.GetAllOrderByDispalyName();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpGet(ApiRoutes.Users.GETUSER + "/{appUserId}")]
        public async Task<ActionResult<AppUser>> GetUser(string appUserId)
        {
            return await _appUsersService.GetById(appUserId);
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Users.GETBIRTHDAYS)]
        public async Task<ActionResult<List<AppUser>>> GetBirthdays()
        {
            var firstDayOfWeek = DateTime.Now.FirstDayOfWeek();
            // AddDay(1) because a DateTime used as a date is really the very beginning of that day, 
            // and doesn't extend to the end of the day
            var lastDayOfWeek = DateTime.Now.LastDayOfWeek().AddDays(1);

            return await _appUsersService.GetBirthdaysForOneWeek(firstDayOfWeek, lastDayOfWeek);
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpPut(ApiRoutes.Users.UPDATE + "/{appUserId:int}")]
        public async Task<ActionResult<AppUser>> PutUserAsync(int appUserId, AppUserViewModel appUser)
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