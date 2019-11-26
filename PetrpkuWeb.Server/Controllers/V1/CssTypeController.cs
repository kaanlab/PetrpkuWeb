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
    public class CssTypeController : ControllerBase
    {
        private readonly ICssTypeService _cssTypeService;
        private readonly IMapper _mapper;

        public CssTypeController(ICssTypeService cssTypeService, IMapper mapper)
        {
            _cssTypeService = cssTypeService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.CssType.GETALL)]
        public async Task<ActionResult> GetAll()
        {
            var cssType = await _cssTypeService.GetAll();

            return Ok(_mapper.Map<CssTypeViewModel>(cssType));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.CssType.SHOW + "/{cssTypeId:int}")]
        public async Task<ActionResult> GetCssType(int cssTypeId)
        {
            var cssType = await _cssTypeService.GetCssType(cssTypeId);

            if (cssType is null)
                return NotFound();

            return Ok(_mapper.Map<CssTypeViewModel>(cssType));
        }

        [Authorize(Roles = AuthRoles.ADMIN)]
        [HttpPost(ApiRoutes.CssType.CREATE)]
        public async Task<ActionResult> CreateCssType(CssTypeViewModel cssTypeViewModel)
        {
            if (cssTypeViewModel is null)
                return BadRequest();

            var cssType = _mapper.Map<CssType>(cssTypeViewModel);
            var created = await _cssTypeService.Create(cssType);

            if(created)
                return Ok(_mapper.Map<CssTypeViewModel>(cssType));

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN)]
        [HttpPut(ApiRoutes.CssType.UPDATE + "/{cssTypeId:int}")]
        public async Task<ActionResult> UpdateCssType(int cssTypeViewModelId, CssTypeViewModel cssTypeViewModel)
        {
            if (cssTypeViewModelId == cssTypeViewModel.CssTypeId)
            {
                var cssType = _mapper.Map<CssType>(cssTypeViewModel);
                var updated = await _cssTypeService.Update(cssType);
                
                if (updated)
                    return Ok(_mapper.Map<CssTypeViewModel>(cssType));
            }

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN)]
        [HttpDelete(ApiRoutes.CssType.DELETE + "/{cssTypeId:int}")]
        public async Task<IActionResult> DeleteCssType(int cssTypeId)
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