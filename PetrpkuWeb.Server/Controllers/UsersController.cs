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
    }
}