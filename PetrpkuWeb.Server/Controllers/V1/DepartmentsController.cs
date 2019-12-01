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
using PetrpkuWeb.Shared.ViewModels.CatalogRegion;

namespace PetrpkuWeb.Server.Controllers.V1
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ITypeService<Department> _departmentTypeService;
        private readonly IMapper _mapper;

        public DepartmentsController(ITypeService<Department> departmentTypeService, IMapper mapper)
        {
            _departmentTypeService = departmentTypeService;
            _mapper = mapper;
        }

        //[Authorize(Roles = AuthRole.ANY)]
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Departments.ALL)]
        public async Task<ActionResult> GetDepartments()
        {
            var department = await _departmentTypeService.GetAll();

            return Ok(_mapper.Map<IEnumerable<CatalogDepartmentView>>(department));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Departments.SHOW + "/{departmentId:int}")]
        public async Task<ActionResult<Department>> GetDepartment(int departmentId)
        {

            var department = await _departmentTypeService.GetOne(departmentId);

            if (department is null)
                return NotFound();

            return Ok(_mapper.Map<DepartmentViewModel>(department));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpPost(ApiRoutes.Departments.CREATE)]
        public async Task<ActionResult<Department>> CreateDepartmentAsync(CatalogDepartmentView catalogDepartmentView)
        {
            if (catalogDepartmentView is null)
                return BadRequest();

            var department = _mapper.Map<Department>(catalogDepartmentView);

            var created = await _departmentTypeService.Create(department);

            if(created)
                return Ok(_mapper.Map<CatalogDepartmentView>(department));

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpPut(ApiRoutes.Departments.UPDATE + "/{catalogDepartmentViewId:int}")]
        public async Task<ActionResult> UpdateDepartmentAsync(int catalogDepartmentViewId, CatalogDepartmentView catalogDepartmentView)
        {
            if (catalogDepartmentViewId == catalogDepartmentView.DepartmentId)
            {
                var department = await _departmentTypeService.GetOne(catalogDepartmentViewId);
                var updatedDepartment = _mapper.Map(catalogDepartmentView, department);
                var updated = await _departmentTypeService.Update(updatedDepartment);

                if(updated)
                    return Ok(_mapper.Map<CatalogDepartmentView>(department));
            }

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpDelete(ApiRoutes.Departments.DELETE + "/{departmentId:int}")]
        public async Task<ActionResult> DeleteDepartmentAsync(int departmentId)
        {
            if (ModelState.IsValid)
            {
                var deleted = await _departmentTypeService.Delete(departmentId);

                if (deleted)
                    return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}