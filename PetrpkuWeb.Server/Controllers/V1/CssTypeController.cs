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
    public class CssTypeController : ControllerBase
    {
        private readonly ITypeService<CssType> _cssTypeService;
        private readonly IMapper _mapper;

        public CssTypeController(ITypeService<CssType> cssTypeService, IMapper mapper)
        {
            _cssTypeService = cssTypeService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.CssType.ALL)]
        public async Task<ActionResult> GetAll()
        {
            var cssType = await _cssTypeService.GetAll();

            return Ok(_mapper.Map<IEnumerable<CssTypeView>>(cssType));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.CssType.SHOW + "/{cssTypeId:int}")]
        public async Task<ActionResult> GetCssType(int cssTypeId)
        {
            var cssType = await _cssTypeService.GetOne(cssTypeId);

            if (cssType is null)
                return NotFound();

            return Ok(_mapper.Map<CssTypeView>(cssType));
        }

        [Authorize(Roles = AuthRoles.ADMIN)]
        [HttpPost(ApiRoutes.CssType.CREATE)]
        public async Task<ActionResult> CreateCssType(CssTypeView cssTypeView)
        {
            if (cssTypeView is null)
                return NotFound();

            var cssType = _mapper.Map<CssType>(cssTypeView);
            var created = await _cssTypeService.Create(cssType);

            if(created)
                return Ok(_mapper.Map<CssTypeView>(cssType));

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN)]
        [HttpPut(ApiRoutes.CssType.UPDATE + "/{cssTypeViewId:int}")]
        public async Task<ActionResult> UpdateCssType(int cssTypeViewId, CssTypeView cssTypeView)
        {
            if (cssTypeViewId == cssTypeView.CssTypeId)
            {
                var cssType = _mapper.Map<CssType>(cssTypeView);
                var updated = await _cssTypeService.Update(cssType);
                
                if (updated)
                    return Ok(_mapper.Map<CssTypeView>(cssType));
            }

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN)]
        [HttpDelete(ApiRoutes.CssType.DELETE + "/{cssTypeId:int}")]
        public async Task<ActionResult> DeleteCssType(int cssTypeId)
        {
            if (ModelState.IsValid)
            {
                var deleted = await _cssTypeService.Delete(cssTypeId);
                
                if (deleted)
                    return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}