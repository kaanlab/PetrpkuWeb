using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Shared.Models;

namespace PetrpkuWeb.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        [HttpGet("all/active")]
        public async Task<ActionResult<List<AppUser>>> GetActiveUsers()
        {
            return await _db.AppUsers
                .Include(a => a.Avatar)
                .Include(b => b.Building)
                .Include(d => d.Department)
                .Where(u => u.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        [Authorize(Roles = AuthRole.ANY)]
        [HttpGet("duty/active")]
        public async Task<ActionResult<List<AppUser>>> GetActiveDuties()
        {
            return await _db.AppUsers
                .Include(a => a.Avatar)
                .Include(b => b.Building)
                .Include(d => d.Department)
                .Where(u => u.IsActive && u.IsDuty)
                .AsNoTracking()
                .ToListAsync();
        }

        [Authorize(Roles = AuthRole.ADMIN)]
        [HttpGet("all/disabled")]
        public async Task<ActionResult<List<AppUser>>> GetDisabledUsers()
        {
            return await _db.AppUsers
                .Include(a => a.Avatar)
                .Where(u => u.IsActive == false)
                .AsNoTracking()
                .ToListAsync();
        }

        [Authorize(Roles = AuthRole.ANY)]
        [HttpGet("user/{appUserId:int}")]
        public async Task<ActionResult<AppUser>> GetUser(int appUserId)
        {
            return await _db.AppUsers
                .Include(d => d.DaysOfDuty)
                .Include(a => a.Avatar)
                .Include(b => b.Building)
                .Include(d => d.Department)
                .Include(a => a.Articles)
                    .ThenInclude(atach => atach.Attachments)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.AppUserId == appUserId);
        }

        [AllowAnonymous]
        [HttpGet("birthdaysweek")]
        public async Task<ActionResult<List<AppUser>>> GetUsersBirthdaysForWeek()
        {
            var firstDayOfWeek = DateTime.Now.FirstDayOfWeek();
            // AddDay(1) because a DateTime used as a date is really the very beginning of that day, 
            // and doesn't extend to the end of the day
            var lastDayOfWeek = DateTime.Now.LastDayOfWeek().AddDays(1);

            return await _db.AppUsers
                .Include(a => a.Avatar)
                .Include(d =>d.Department)
                .Where(d => (d.Birthday.DayOfYear >= firstDayOfWeek.DayOfYear && d.Birthday.DayOfYear <= lastDayOfWeek.DayOfYear))
                .OrderBy(o => o.Birthday.DayOfYear)
                .AsNoTracking()
                .ToListAsync();
        }

        [Authorize(Roles = AuthRole.ANY)]
        [HttpPut("user/update/{appUserId:int}")]
        public async Task<ActionResult<AppUser>> PutUserAsync(int appUserId, AppUser appUser)
        {
            if (appUserId == appUser.AppUserId)
            {
                var building = await _db.Buildings.SingleOrDefaultAsync(b => b.BuildingId == appUser.BuildingId);
                var department = await _db.Departments.SingleOrDefaultAsync(d => d.DepartmentId == appUser.DepartmentId);

                appUser.Building = building;
                appUser.Department = department;
                _db.Update(appUser);
                await _db.SaveChangesAsync();
                return Ok(appUser);
            }
            return BadRequest();
        }
    }
}