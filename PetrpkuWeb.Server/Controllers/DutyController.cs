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

        [HttpGet("today")]
        public async Task<ActionResult<Duty>> GetWhoIsDutyToday()
        {
            return await _db.Duties
                .Where(d => d.DayOfDuty.DayOfYear == DateTime.Now.DayOfYear)
                .Include(u => u.AssignedTo)
                .FirstOrDefaultAsync();
        }

        [HttpGet("month/{selectedMonth}/{selectedYear}")]
        public async Task<ActionResult<List<Duty>>> GetDutyMonth([FromRoute] int selectedMonth, [FromRoute] int selectedYear)
        {
            return await _db.Duties
                .Where(d => (d.DayOfDuty.Month == selectedMonth && d.DayOfDuty.Year == selectedYear))
                .Include(u => u.AssignedTo)
                .ToListAsync();
        }

        [HttpPost("createdutylist")]
        public async Task<ActionResult> PostDutyAsync([FromBody] List<Duty> dutyList)
        {
            await _db.Duties.AddRangeAsync(dutyList);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}