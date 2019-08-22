using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly DbStorageContext _db;

        public UsersController(DbStorageContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UserInfo>>> GetUsers()
        {
            return await _db.Users.ToListAsync();
        }

        [HttpGet("birthdaysweek")]
        public async Task<ActionResult<List<UserInfo>>> GetUsersBirthdaysForWeek()
        {
            var firstDayOfWeek = DateTime.Now.FirstDayOfWeek();
            // AddDay(1) because a DateTime used as a date is really the very beginning of that day, 
            // and doesn't extend to the end of the day
            var lastDayOfWeek = DateTime.Now.LastDayOfWeek().AddDays(1);

            return await _db.Users
                .Where(d => (d.Birthday.DayOfYear >= firstDayOfWeek.DayOfYear && d.Birthday.DayOfYear <= lastDayOfWeek.DayOfYear))
                .OrderBy(o => o.Birthday.DayOfYear)
                .ToListAsync();
        }

        [HttpPut("updatebirthday/{userInfoId}")]
        public async Task<IActionResult> PutDutyAsync(int userInfoId, UserInfo user)
        {
            if (userInfoId != user.UserInfoId)
            {
                return BadRequest();
            }

            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}