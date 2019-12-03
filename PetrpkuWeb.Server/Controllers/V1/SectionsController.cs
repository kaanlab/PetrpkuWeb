using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Server.Services;
using PetrpkuWeb.Shared.Contracts.V1;
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Shared.Views;

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

            return Ok(_mapper.Map<IEnumerable<SiteSectionView>>(siteSections));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Sections.ALL_INCLUDE_SUBSECTIONS)]
        public async Task<ActionResult> GetSiteSectionsAndSubSections()
        {
            var siteSections = await _sectionsService.GetSiteSectionsIncludeSubSections();

            return Ok(_mapper.Map<IEnumerable<SiteSectionView>>(siteSections));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Sections.SHOW + "/{siteSectionId:int}")]
        public async Task<ActionResult> GetSiteSection(int siteSectionId)
        {
            var siteSection = await _sectionsService.GetSiteSection(siteSectionId);

            if (siteSection is null)
                return NotFound();

            return Ok(_mapper.Map<SiteSectionView>(siteSection));
        }

        [Authorize(Roles = AuthRoles.ADMIN_PUBLISHER)]
        [HttpPost(ApiRoutes.Sections.CREATE)]
        public async Task<ActionResult> AddSiteSectionAsync(SiteSectionView siteSectionView)
        {
            if (siteSectionView is null)
                return BadRequest();

            var siteSection = _mapper.Map<SiteSection>(siteSectionView);

            var created = await _sectionsService.Create(siteSection);

            if (created)
                return Ok(_mapper.Map<SiteSectionView>(siteSection));

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_PUBLISHER)]
        [HttpPut(ApiRoutes.Sections.UPDATE + "/{siteSectionViewId:int}")]
        public async Task<ActionResult> UpdateSiteSectionAsync(int siteSectionViewId, SiteSectionView siteSectionView)
        {
            var siteSection = await _sectionsService.GetSiteSection(siteSectionViewId);

            if (siteSection is null)
                return BadRequest();

            if (siteSection.SiteSectionId == siteSectionView.SiteSectionId)
            {
                var updatedSiteSection = _mapper.Map(siteSectionView, siteSection);
                var updated = await _sectionsService.Update(updatedSiteSection);

                if (updated)
                    return Ok(_mapper.Map<SiteSectionView>(updatedSiteSection));
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

            return Ok(_mapper.Map<IEnumerable<SiteSubSectionView>>(subSections));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Sections.SUBSECTIONS + "/{siteSectionId:int}")]
        public async Task<ActionResult> GetSubSiteSectionsForSiteSection(int siteSectionId)
        {
            var subSections = await _sectionsService.GetSubsectionsForSection(siteSectionId);

            return Ok(_mapper.Map<IEnumerable<SiteSectionView>>(subSections));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Sections.SUBSECTION_SHOW + "/{siteSubSectionId:int}")]
        public async Task<ActionResult> GetSiteSubSection(int siteSubSectionId)
        {
            var siteSubSection = await _sectionsService.GetSiteSubSection(siteSubSectionId);

            if (siteSubSection is null)
                return NotFound();

            return Ok(_mapper.Map<SiteSubSectionView>(siteSubSection));
        }

        [Authorize(Roles = AuthRoles.ADMIN_PUBLISHER)]
        [HttpPost(ApiRoutes.Sections.SUBSECTION_CREATE)]
        public async Task<ActionResult> CreateSiteSubSectionAsync(SiteSubSectionView siteSubSectionView)
        {
            if (siteSubSectionView is null)
                return NotFound();

            var siteSubSection = _mapper.Map<SiteSubsection>(siteSubSectionView);
            var created = await _sectionsService.CreateSubSections(siteSubSection);

            if (created)
                return Ok(_mapper.Map<SiteSubSectionView>(siteSubSection));

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_PUBLISHER)]
        [HttpPut(ApiRoutes.Sections.UPDATE + "/{siteSubSectionViewId:int}")]
        public async Task<ActionResult> UpdateSiteSubSectionAsync(int siteSubSectionViewId, SiteSubSectionView siteSubSectionView)
        {
            var siteSubSection = await _sectionsService.GetSiteSubSection(siteSubSectionViewId);
            if (siteSubSection is null)
                return BadRequest();

            if (siteSubSection.SiteSubSectionId == siteSubSectionView.SiteSubSectionId)
            {
                var updatedSiteSubSection = _mapper.Map(siteSubSectionView, siteSubSection);
                var updated = await _sectionsService.UpdateSubSection(siteSubSection);

                if (updated)
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
                if (deleted)
                    return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}