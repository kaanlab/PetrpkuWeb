using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetrpkuWeb.Shared.Views;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Server.Services;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.Contracts.V1;

namespace PetrpkuWeb.Server.Controllers.V1
{
    //[Route("api/articles")]
    [ApiController]
    public class DocSectionsController : ControllerBase
    {
        private readonly ITypeService<DocSection> _docSectionTypeService;
        private readonly IMapper _mapper;

        public DocSectionsController(ITypeService<DocSection> docSectionTypeService, IMapper mapper)
        {
            _docSectionTypeService = docSectionTypeService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.DocSection.ALL)]
        public async Task<ActionResult> GetDocSections()
        {
            var docSections = await _docSectionTypeService.GetAll();

            return Ok(_mapper.Map<IEnumerable<DocSectionAppUserDepartmentAttachmentsView>>(docSections));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpPost(ApiRoutes.DocSection.CREATE)]
        public async Task<ActionResult> CreateDocSection(DocSectionAppUserDepartmentAttachmentsView docSectionView)
        {
            if (docSectionView is null)
                return BadRequest();

            var docSection = _mapper.Map<DocSection>(docSectionView);
            var created = await _docSectionTypeService.Create(docSection);
            
           if(created)
                return Ok(_mapper.Map<DocSectionAppUserDepartmentAttachmentsView>(docSection));

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.DocSection.SHOW + "/{docSectionId:int}")]
        public async Task<ActionResult> GetDocSection(int docSectionId)
        {
            var docSection = await _docSectionTypeService.GetOne(docSectionId);

            if (docSection is null)
                return NotFound();

            return Ok(_mapper.Map<DocSectionAppUserDepartmentAttachmentsView>(docSection));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpPut(ApiRoutes.DocSection.UPDATE + "/{docSectionViewId:int}")]
        public async Task<ActionResult> UpdateDocSectionAsync(int docSectionViewId, DocSectionAppUserDepartmentAttachmentsView docSectionView)
        {
            if (docSectionViewId == docSectionView.DocSectionId)
            {
                var docSection = await _docSectionTypeService.GetOne(docSectionViewId);
                var updatedDocSection = _mapper.Map(docSectionView, docSection);
                var updated = await _docSectionTypeService.Update(updatedDocSection);

                if(updated)
                    return Ok(_mapper.Map<DocSectionAppUserDepartmentAttachmentsView>(updatedDocSection));
            }
            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpDelete(ApiRoutes.DocSection.DELETE + "/{docSectionId:int}")]
        public async Task<ActionResult> DeleteDocSection(int docSectionId)
        {
            if (ModelState.IsValid)
            {
                var deleted = await _docSectionTypeService.Delete(docSectionId);

                if(deleted)
                    return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}