﻿using System;
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
    public class PhonebookController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PhonebookController(AppDbContext db)
        {
            _db = db;
        }

        //[Authorize(Roles = AuthRole.ANY)]
        [HttpGet("departments/all")]
        public async Task<ActionResult<List<Department>>> GetDepartments()
        {
            return await _db.Departments
                .Include(u => u.ListOfUsers)
                .AsNoTracking()
                .ToListAsync();
        }

        [Authorize(Roles = AuthRole.ADMIN_KADRY)]
        [HttpPost("department/create")]
        public async Task<ActionResult<Department>> AddDepartmentAsync(Department department)
        {
            if (department is null)
                return BadRequest();

            _db.Departments.Add(department);
            await _db.SaveChangesAsync();

            return Ok(department);
        }

        [Authorize(Roles = AuthRole.ADMIN_KADRY)]
        [HttpPut("department/update/{departmentId:int}")]
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
        [HttpDelete("department/delete/{departmentId:int}")]
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
                return Ok(department);
            }

            return BadRequest(ModelState);
        }

        [Authorize(Roles = AuthRole.ANY)]
        [HttpGet("buildings/all")]
        public async Task<ActionResult<List<Building>>> GetBuildings()
        {
            return await _db.Buildings
                .AsNoTracking()
                .ToListAsync();
        }

        [Authorize(Roles = AuthRole.ADMIN_KADRY)]
        [HttpPost("building/create")]
        public async Task<ActionResult<Building>> AddBuildingAsync(Building building)
        {
            if (building is null)
                return BadRequest();

            _db.Buildings.Add(building);
            await _db.SaveChangesAsync();

            return Ok(building);
        }

        [Authorize(Roles = AuthRole.ADMIN_KADRY)]
        [HttpPut("building/update/{buildingId:int}")]
        public async Task<ActionResult> PutBuildingAsync(int buildingId, Building building)
        {
            if (buildingId == building.BuildingId)
            {
                _db.Attach(building).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Ok(building);
            }

            return BadRequest();
        }

        [Authorize(Roles = AuthRole.ADMIN_KADRY)]
        [HttpDelete("building/delete/{buildingId:int}")]
        public async Task<IActionResult> DeleteBuildingAsync(int buildingId)
        {
            if (ModelState.IsValid)
            {
                var building = await _db.Buildings
                    .SingleOrDefaultAsync(b => b.BuildingId == buildingId);

                if (building is null)
                {
                    return NotFound();
                }

                _db.Buildings.Remove(building);
                await _db.SaveChangesAsync();
                return Ok(building);
            }

            return BadRequest(ModelState);
        }
    }
}