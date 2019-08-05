using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Shared.Models;

namespace PetrpkuWeb.Server.Controllers
{
    [Route("api/duty")]
    [ApiController]
    public class DutyController : ControllerBase
    {
        private readonly DbStorageContext _db;

        public DutyController(DbStorageContext db)
        {
            _db = db;
        }

        [HttpGet("whoisdutytoday")]
        public async Task<ActionResult<Duty>> GetWhoIsDutyToday()
        {
            return await _db.Duties
                .Where(d => d.DayOfDuty.DayOfYear == DateTime.Now.DayOfYear)
                .Include(u => u.AssignedTo)
                .FirstOrDefaultAsync();
        }
    }
}