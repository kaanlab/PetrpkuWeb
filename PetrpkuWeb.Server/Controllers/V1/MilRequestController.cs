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
    //[Route("api/[controller]")]
    [ApiController]
    public class MilRequestController : ControllerBase
    {
        private readonly IMilRequestService _milRequestService;
        private readonly IMapper _mapper;

        public MilRequestController(IMilRequestService milRequestService, IMapper mapper)
        {
            _milRequestService = milRequestService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.MilRequest.ALL)]
        public async Task<ActionResult> GetMilRequest()
        {
            var milRequests = await _milRequestService.GetAll();

            return Ok(_mapper.Map<IEnumerable<MilRequestView>>(milRequests));
        }
               
        [Authorize(Roles = AuthRoles.ADMIN_PUBLISHER)]
        [HttpPost(ApiRoutes.MilRequest.CREATE)]
        public async Task<ActionResult> CreateMilRequest(MilRequestView milRequestView)
        {
            if (milRequestView is null)
                return BadRequest();

            var milRequest = _mapper.Map<MilRequest>(milRequestView);
            milRequest.CreateDate = DateTime.Now;

            var created = await _milRequestService.Create(milRequest);

            if(created)
                return Ok(_mapper.Map<MilRequestView>(milRequest));

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.MilRequest.SHOW + "/{milRequestId:int}")]
        public async Task<ActionResult> GetMilRequest(int milRequestId)
        {
            var milRequest = await _milRequestService.GetOne(milRequestId);
            
            if (milRequest is null)
                return NotFound();

            return Ok(_mapper.Map<MilRequestView>(milRequest));
        }
    }
}