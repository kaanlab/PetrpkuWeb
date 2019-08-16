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
        public async Task<ActionResult<Duty>> GetWhoIsDutyTodayAsync()
        {
            return await _db.Duties
                .Where(d => d.DayOfDuty.DayOfYear == DateTime.Now.DayOfYear)
                .Include(u => u.AssignedTo)
                .FirstOrDefaultAsync();
        }

        [HttpGet("month/{selectedMonth}/{selectedYear}")]
        public async Task<ActionResult<List<Duty>>> GetDutyMonthAsync([FromRoute] int selectedMonth, [FromRoute] int selectedYear)
        {
            return await _db.Duties
                .Where(d => (d.DayOfDuty.Month == selectedMonth && d.DayOfDuty.Year == selectedYear))
                .Include(u => u.AssignedTo)
                .OrderBy(d => d.DayOfDuty)
                .ToListAsync();
        }

        [HttpPost("createdutylist")]
        public async Task<ActionResult> PostDutyListAsync([FromBody] List<Duty> dutyList)
        {
            await _db.Duties.AddRangeAsync(dutyList);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Duty>> PostDutyAsync(Duty duty)
        {
            _db.Duties.Add(duty);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("update/{dutyId}")]
        public async Task<IActionResult> PutDutyAsync(int dutyId, Duty duty)
        {
            if (dutyId != duty.DutyId)
            {
                return BadRequest();
            }

            _db.Entry(duty).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{dutyUserId}")]
        public async Task<ActionResult> DeleteDutyAsync([FromRoute] int dutyUserId)
        {
            var dutyUser = await _db.Duties.FindAsync(dutyUserId);

            if (dutyUser == null)
            {
                return NotFound();
            }

            _db.Duties.Remove(dutyUser);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}