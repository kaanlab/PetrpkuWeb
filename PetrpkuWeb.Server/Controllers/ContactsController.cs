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
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ContactsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<AppUser>>> GetContacts()
        {
            return await _db.AppUsers
                .Include(a => a.Avatar)
                .Include(d => d.Department)
                .Include(b => b.Building)
                .Where(u => u.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpPut("update/{appUserId:int}")]
        public async Task<IActionResult> PutUserAsync(int appUserId, AppUser appUser)
        {
            _db.Update(appUser);
            await _db.SaveChangesAsync();
            return NoContent();
        }
       
    }
}