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
    public class CssTypeController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CssTypeController(AppDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<ActionResult<List<CssType>>> GetCssType()
        {
            return await _db.CssTypes
                .AsNoTracking()
                .ToListAsync();
        }


        [AllowAnonymous]
        [HttpGet("show/{cssTypeId:int}")]
        public async Task<ActionResult<CssType>> GetSiteSection(int cssTypeId)
        {
            var cssType = await _db.CssTypes
                 .AsNoTracking()
                 .SingleOrDefaultAsync(ct => ct.CssTypeId == cssTypeId);

            if (cssType is null)
                return BadRequest();

            return Ok(cssType);
        }

        [Authorize(Roles = AuthRole.ADMIN)]
        [HttpPost("create")]
        public async Task<ActionResult<CssType>> AddSiteSectionAsync(CssType cssType)
        {
            if (cssType is null)
                return BadRequest();

            _db.CssTypes.Add(cssType);
            await _db.SaveChangesAsync();

            return Ok(cssType);
        }

        [Authorize(Roles = AuthRole.ADMIN)]
        [HttpPut("update/{siteSectionId:int}")]
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

        [Authorize(Roles = AuthRole.ADMIN)]
        [HttpDelete("delete/{cssTypeId:int}")]
        public async Task<IActionResult> DeleteSiteSectionAsync(int cssTypeId)
        {
            if (ModelState.IsValid)
            {
                var cssType = await _db.CssTypes
                    .SingleOrDefaultAsync(cs => cs.CssTypeId == cssTypeId);

                if (cssType is null)
                {
                    return NotFound();
                }

                _db.CssTypes.Remove(cssType);
                await _db.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}