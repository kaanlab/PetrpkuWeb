using System;
using System.Collections.Generic;
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
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public DepartmentsController(AppDbContext db)
        {
            _db = db;
        }

        //[Authorize(Roles = AuthRole.ANY)]
        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<ActionResult<List<Department>>> GetDepartments()
        {
            return await _db.Departments
                .Include(u => u.ListOfUsers)
                .AsNoTracking()
                .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("show/{departmentId:int}")]
        public async Task<ActionResult<Department>> GetDepartment(int departmentId)
        {
            var department = await _db.Departments
                 .Include(u => u.ListOfUsers)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(u => u.DepartmentId == departmentId);

            if (department is null)
                return BadRequest();

            return Ok(department);
        }

        [Authorize(Roles = AuthRole.ADMIN_KADRY)]
        [HttpPost("create")]
        public async Task<ActionResult<Department>> AddDepartmentAsync(Department department)
        {
            if (department is null)
                return BadRequest();

            //department.IsHidden = false;
            _db.Departments.Add(department);
            await _db.SaveChangesAsync();

            return Ok(department);
        }

        [Authorize(Roles = AuthRole.ADMIN_KADRY)]
        [HttpPut("update/{departmentId:int}")]
        public async Task<ActionResult> PutDepartmentAsync(int departmentId, Department department)
        {
            if (departmentId == department.DepartmentId)
            {
                _db.Attach(department).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Ok(department);
            }

            return BadRequest();
        }

        [Authorize(Roles = AuthRole.ADMIN_KADRY)]
        [HttpDelete("delete/{departmentId:int}")]
        public async Task<IActionResult> DeleteDepartmentAsync(int departmentId)
        {
            if (ModelState.IsValid)
            {
                var department = await _db.Departments
                    .SingleOrDefaultAsync(d => d.DepartmentId == departmentId);

                if (department is null)
                {
                    return NotFound();
                }

                _db.Departments.Remove(department);
                await _db.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}