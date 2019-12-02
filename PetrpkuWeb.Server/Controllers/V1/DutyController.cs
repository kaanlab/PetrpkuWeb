using System;
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
    //[Route("api/duty")]
    [ApiController]
    public class DutyController : ControllerBase
    {
        private readonly IDutyService _dutyService;
        private readonly IMapper _mapper;

        public DutyController(IDutyService dutyService, IMapper mapper)
        {
            _dutyService = dutyService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Duty.TODAY)]
        public async Task<ActionResult> GetWhoIsDutyTodayAsync()
        {
            var duty = await _dutyService.DutyToday();

            if (duty is null)
                return NotFound();

            return Ok(_mapper.Map<DutyAppUserView>(duty));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Duty.MONTH + "/{selectedMonth:int}/{selectedYear:int}")]
        public async Task<ActionResult> GetDutyMonthAsync([FromRoute] int selectedMonth, [FromRoute] int selectedYear)
        {
            var dutys = await _dutyService.DutyMonth(selectedMonth, selectedYear);

            return Ok(_mapper.Map<IEnumerable<DutyAppUserView>>(dutys));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpGet(ApiRoutes.Duty.GETFILE + "/{selectedMonth:int}/{selectedYear:int}")]
        public async Task<ActionResult> GetFileAsync([FromRoute] int selectedMonth, [FromRoute] int selectedYear)
        {
            var file = await _dutyService.GetFileAsync(selectedMonth, selectedYear);

            return File(file, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"API_{DateTime.Now.ToString("dd-MM-yyyy")}.docx");
        }

        //[Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        //[HttpPost("createdutylist")]
        //public async Task<ActionResult> PostDutyListAsync(List<Duty> dutyList)
        //{
        //    await _db.Duties.AddRangeAsync(dutyList);
        //    await _db.SaveChangesAsync();
        //    return Ok(dutyList);
        //}

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpPost(ApiRoutes.Duty.CREATE)]
        public async Task<ActionResult> CreateDutyAsync(DutyAppUserView dutyView)
        {
            if (dutyView is null)
                return NotFound();

            var duty = _mapper.Map<Duty>(dutyView);

            var created = await _dutyService.Create(duty);

            if (created)
                return Ok(_mapper.Map<DutyAppUserView>(duty));

            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpPut(ApiRoutes.Duty.UPDATE + "/{dutyViewId:int}")]
        public async Task<ActionResult> UpdateDutyAsync(int dutyViewId, DutyAppUserView dutyView)
        {
            if (dutyViewId == dutyView.DutyId)
            {
                var duty = await _dutyService.GetOne(dutyViewId);
                var dutyToUpdate = _mapper.Map(dutyView, duty);
                var updated = await _dutyService.Update(dutyToUpdate);

                if (updated)
                    return Ok(_mapper.Map<DutyAppUserView>(duty));
            }

            return NoContent();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY)]
        [HttpDelete(ApiRoutes.Duty.DELETE + "/{dutyId:int}")]
        public async Task<ActionResult> DeleteDutyAsync([FromRoute] int dutyId)
        {
            if (ModelState.IsValid)
            {
                var deleted = await _dutyService.Delete(dutyId);

                if (deleted)
                    return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}