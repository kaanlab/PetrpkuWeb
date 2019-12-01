using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Server.Services;
using PetrpkuWeb.Shared.Contracts.V1;
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Shared.Models;
using PetrpkuWeb.Shared.ViewModels;

namespace PetrpkuWeb.Server.Controllers.V1
{
    //[Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionsService _sectionsService;
        private readonly IMapper _mapper;

        public SectionsController(ISectionsService sectionsService, IMapper mapper)
        {
            _sectionsService = sectionsService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Sections.ALL)]
        public async Task<ActionResult> GetSiteSections()
        {
            var siteSections = await _sectionsService.GetSiteSections();

            return Ok(_mapper.Map<IEnumerable<SiteSectionViewModel>>(siteSections));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Sections.ALL_INCLUDE_SUBSECTIONS)]
        public async Task<ActionResult> GetSiteSectionsAndSubSections()
        {
            var siteSections = await _sectionsService.GetSiteSectionsIncludeSubSections();

            return Ok(_mapper.Map<IEnumerable<SiteSectionViewModel>>(siteSections));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Sections.SHOW + "/{siteSectionId:int}")]
        public async Task<ActionResult> GetSiteSection(int siteSectionId)
        {
            var siteSection = await _sectionsService.GetSiteSection(siteSectionId);

            if (siteSection is null)
                return NotFound();

            return Ok(_mapper.Map<SiteSectionViewModel>(siteSection));
        }

        [Authorize(Roles = AuthRoles.ADMIN_PUBLISHER)]
        [HttpPost(ApiRoutes.Sections.CREATE)]
        public async Task<ActionResult> AddSiteSectionAsync(SiteSectionViewModel siteSectionViewModel)
        {
            if (siteSectionViewModel is null)
                return NotFound();

            var siteSection = _mapper.Map<SiteSection>(siteSectionViewModel);

            var created = await _sectionsService.Create(siteSection);

            if (created)
                return Ok(_mapper.Map<SiteSectionViewModel>(siteSection));

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_PUBLISHER)]
        [HttpPut(ApiRoutes.Sections.UPDATE + "/{siteSectionViewModelId:int}")]
        public async Task<ActionResult> UpdateSiteSectionAsync(int siteSectionViewModelId, SiteSectionViewModel siteSectionViewModel)
        {
            if (siteSectionViewModelId == siteSectionViewModel.SiteSectionId)
            {
                var siteSection = _mapper.Map<SiteSection>(siteSectionViewModel);
                var updated = await _sectionsService.Update(siteSection);

                if (updated)
                    return Ok(_mapper.Map<SiteSectionViewModel>(siteSection));
            }

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_PUBLISHER)]
        [HttpDelete(ApiRoutes.Sections.DELETE + "/{siteSectionId:int}")]
        public async Task<IActionResult> DeleteSiteSectionAsync(int siteSectionId)
        {
            if (ModelState.IsValid)
            {
                var deleted = await _sectionsService.Delete(siteSectionId);
                if (deleted)
                    return NoContent();
            }

            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Sections.SUBSECTION_ALL)]
        public async Task<ActionResult> GetSubSiteSections()
        {
            var subSections = await _sectionsService.GetSubSectionsIncludeSections();

            return Ok(_mapper.Map<IEnumerable<SiteSubSectionViewModel>>(subSections));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Sections.SUBSECTIONS + "/{siteSectionId:int}")]
        public async Task<ActionResult> GetSubSiteSectionsForSiteSection(int siteSectionId)
        {
            var subSections = await _sectionsService.GetSubsectionsForSection(siteSectionId);

            return Ok(_mapper.Map<IEnumerable<SiteSectionViewModel>>(subSections));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Sections.SUBSECTION_SHOW + "/{siteSubSectionId:int}")]
        public async Task<ActionResult> GetSiteSubSection(int siteSubSectionId)
        {
            var siteSubSection = await _sectionsService.GetSiteSubSection(siteSubSectionId);

            if (siteSubSection is null)
                return NotFound();

            return Ok(_mapper.Map<SiteSubSectionViewModel>(siteSubSection));
        }

        [Authorize(Roles = AuthRoles.ADMIN_PUBLISHER)]
        [HttpPost(ApiRoutes.Sections.SUBSECTION_CREATE)]
        public async Task<ActionResult> CreateSiteSubSectionAsync(SiteSubSectionViewModel siteSubSectionViewModel)
        {
            if (siteSubSectionViewModel is null)
                return NotFound();

            var siteSubSection = _mapper.Map<SiteSubsection>(siteSubSectionViewModel);
            var created = await _sectionsService.CreateSubSections(siteSubSection);

            if (created)
                return Ok(_mapper.Map<SiteSubSectionViewModel>(siteSubSection));

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_PUBLISHER)]
        [HttpPut(ApiRoutes.Sections.UPDATE + "/{siteSubSectionId:int}")]
        public async Task<ActionResult> UpdateSiteSubSectionAsync(int siteSubSectionViewModelId, SiteSubsection siteSubSectionViewModel)
        {
            if (siteSubSectionViewModelId == siteSubSectionViewModel.SiteSubSectionId)
            {
                var siteSubSection = _mapper.Map<SiteSubsection>(siteSubSectionViewModel);
                var updated = await _sectionsService.UpdateSubSection(siteSubSection);

                if(updated)
                    return Ok(siteSubSection);
            }

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_PUBLISHER)]
        [HttpDelete(ApiRoutes.Sections.SUBSECTION_DELETE + "/{siteSubSectionViewModelId:int}")]
        public async Task<IActionResult> DeleteSiteSubSectionAsync(int siteSubSectionViewModelId)
        {
            if (ModelState.IsValid)
            {
                var deleted = await _sectionsService.DeleteSubSection(siteSubSectionViewModelId);
                if(deleted)
                    return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}