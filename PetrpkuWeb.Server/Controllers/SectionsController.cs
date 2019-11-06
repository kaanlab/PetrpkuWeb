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
    public class SectionsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SectionsController(AppDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        [HttpGet("sitesections/all")]
        public async Task<ActionResult<List<SiteSection>>> GetSiteSections()
        {
            return await _db.SiteSections                
                .AsNoTracking()
                .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("sitesection/show/{siteSectionId:int}")]
        public async Task<ActionResult<SiteSection>> GetSiteSection(int siteSectionId)
        {
            var department = await _db.SiteSections
                 .AsNoTracking()
                 .SingleOrDefaultAsync(u => u.SiteSectionId == siteSectionId);

            if (department is null)
                return BadRequest();

            return Ok(department);
        }

        [Authorize(Roles = AuthRole.ADMIN_PUBLISHER)]
        [HttpPost("sitesection/create")]
        public async Task<ActionResult<SiteSection>> AddSiteSectionAsync(SiteSection siteSection)
        {
            if (siteSection is null)
                return BadRequest();

            _db.SiteSections.Add(siteSection);
            await _db.SaveChangesAsync();

            return Ok(siteSection);
        }

        [Authorize(Roles = AuthRole.ADMIN_PUBLISHER)]
        [HttpPut("sitesection/update/{siteSectionId:int}")]
        public async Task<ActionResult> PutSiteSectionAsync(int siteSectionId, SiteSection siteSection)
        {
            if (siteSectionId == siteSection.SiteSectionId)
            {
                _db.Attach(siteSection).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Ok(siteSection);
            }

            return BadRequest();
        }

        [Authorize(Roles = AuthRole.ADMIN_PUBLISHER)]
        [HttpDelete("sitesection/delete/{siteSectionId:int}")]
        public async Task<IActionResult> DeleteSiteSectionAsync(int siteSectionId)
        {
            if (ModelState.IsValid)
            {
                var siteSection = await _db.SiteSections
                    .SingleOrDefaultAsync(s => s.SiteSectionId == siteSectionId);

                if (siteSection is null)
                {
                    return NotFound();
                }

                _db.SiteSections.Remove(siteSection);
                await _db.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpGet("sitesubsections/all")]
        public async Task<ActionResult<List<SiteSubsection>>> GetSubSiteSections()
        {
            return await _db.SiteSubsections
                .Include(s => s.SiteSection)
                .AsNoTracking()
                .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("sitesubsection/show/{siteSectionId:int}")]
        public async Task<ActionResult<SiteSubsection>> GetSiteSubSection(int siteSubSectionId)
        {
            var department = await _db.SiteSubsections
                .Include(s => s.SiteSection)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(u => u.SiteSubsectionId == siteSubSectionId);

            if (department is null)
                return BadRequest();

            return Ok(department);
        }

        [Authorize(Roles = AuthRole.ADMIN_PUBLISHER)]
        [HttpPost("sitesection/create")]
        public async Task<ActionResult<SiteSubsection>> AddSiteSubSectionAsync(SiteSubsection siteSubSection)
        {
            if (siteSubSection is null)
                return BadRequest();

            _db.SiteSubsections.Add(siteSubSection);
            await _db.SaveChangesAsync();

            return Ok(siteSubSection);
        }

        [Authorize(Roles = AuthRole.ADMIN_PUBLISHER)]
        [HttpPut("sitesection/update/{siteSubSectionId:int}")]
        public async Task<ActionResult> PutSiteSectionAsync(int siteSubSectionId, SiteSubsection siteSubSection)
        {
            if (siteSubSectionId == siteSubSection.SiteSectionId)
            {
                _db.Attach(siteSubSection).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Ok(siteSubSection);
            }

            return BadRequest();
        }

        [Authorize(Roles = AuthRole.ADMIN_PUBLISHER)]
        [HttpDelete("sitesection/delete/{siteSectionId:int}")]
        public async Task<IActionResult> DeleteSiteSubSectionAsync(int siteSubSectionId)
        {
            if (ModelState.IsValid)
            {
                var siteSubSection = await _db.SiteSubsections
                    .SingleOrDefaultAsync(s => s.SiteSubsectionId == siteSubSectionId);

                if (siteSubSection is null)
                {
                    return NotFound();
                }

                _db.SiteSubsections.Remove(siteSubSection);
                await _db.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}