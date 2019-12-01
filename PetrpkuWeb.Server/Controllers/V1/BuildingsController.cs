using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Server.Services;
using PetrpkuWeb.Shared.Contracts.V1;
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Shared.ViewModels;

namespace PetrpkuWeb.Server.Controllers.V1
{
    //[Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly ITypeService<Building> _buildingTypeService;
        private readonly IMapper _mapper;

        public BuildingsController(ITypeService<Building> buildingTypeService, IMapper mapper)
        {
            _buildingTypeService = buildingTypeService;
            _mapper = mapper;
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpGet(ApiRoutes.Buildings.ALL)]
        public async Task<ActionResult> GetBuildings()
        {
            var buildings = await _buildingTypeService.GetAll();

            return Ok(_mapper.Map<IEnumerable<BuildingViewModel>>(buildings));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpPost(ApiRoutes.Buildings.CREATE)]
        public async Task<ActionResult> AddBuildingAsync(BuildingViewModel buildingViewModel)
        {
            if (buildingViewModel is null)
                return NotFound();

            var building = _mapper.Map<Building>(buildingViewModel);
            var created = await _buildingTypeService.Create(building);

            if(created)
                return Ok(_mapper.Map<BuildingViewModel>(building));

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpPut(ApiRoutes.Buildings.UPDATE + "/{buildingViewModelId:int}")]
        public async Task<ActionResult> UpdateBuildingAsync(int buildingViewModelId, BuildingViewModel buildingViewModel)
        {
            if (buildingViewModelId == buildingViewModel.BuildingId)
            {
                var building = _mapper.Map<Building>(buildingViewModel);
                var updated = await _buildingTypeService.Update(building);

                if(updated)
                    return Ok(_mapper.Map<BuildingViewModel>(building));
            }

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpDelete(ApiRoutes.Buildings.DELETE + "/{buildingId:int}")]
        public async Task<ActionResult> DeleteBuildingAsync(int buildingId)
        {
            if (ModelState.IsValid)
            {
                var deleted = await _buildingTypeService.Delete(buildingId);
                
                if(deleted)
                    return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}