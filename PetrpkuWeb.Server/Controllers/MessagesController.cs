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
using PetrpkuWeb.Shared.Extensions;
using PetrpkuWeb.Shared.Models;
using PetrpkuWeb.Shared.ViewModels;

namespace PetrpkuWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public MessagesController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<ActionResult<List<Message>>> GetMessages()
        {
            return await _db.Messages
                .Include(s => s.Section)
                .Include(s => s.Subsection)
                .Include(a => a.Attachments)
                .Include(a => a.Author)
                .Include(ch => ch.Checked)
                    .ThenInclude(u => u.AssosiatedUser)
                .Include(s => s.Sent)
                    .ThenInclude(u => u.AssosiatedUser)
                .Include(p => p.Published)
                    .ThenInclude(u => u.AssosiatedUser)
                .OrderByDescending(d => d.CreateDate)
                .AsNoTracking()
                .ToListAsync();
        }
               
        [Authorize(Roles = AuthRole.ADMIN_PUBLISHER)]
        [HttpPost("create")]
        public async Task<ActionResult<Article>> CreateMessage(MessageViewModel messageVM)
        {
            if (messageVM is null)
                return BadRequest();

            var message = _mapper.Map<Message>(messageVM);

            message.CreateDate = DateTime.Now;

            _db.Attachments.UpdateRange(message.Attachments);
            _db.SaveChanges();

            await _db.Messages.AddAsync(message);
            await _db.SaveChangesAsync();

            return Ok(message);
        }

        [AllowAnonymous]
        [HttpGet("show/{messageId:int}")]
        public async Task<ActionResult<List<Message>>> GetMessage(int messageId)
        {
            var message = await _db.Messages
                .Include(s => s.Section)
                .Include(s => s.Subsection)
                .Include(a => a.Attachments)
                .Include(a => a.Author)
                .Include(ch => ch.Checked)
                    .ThenInclude(u => u.AssosiatedUser)
                .Include(s => s.Sent)
                    .ThenInclude(u => u.AssosiatedUser)
                .Include(p => p.Published)
                    .ThenInclude(u => u.AssosiatedUser)
                .OrderByDescending(d => d.CreateDate)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.MessageId == messageId);
            
            if (message is null)
                return BadRequest();

            return Ok(message);
        }
    }
}