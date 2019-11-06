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
                .Include(s => s.Sent)
                .Include(p => p.Published)
                .OrderByDescending(d => d.CreateDate)
                .AsNoTracking()
                .ToListAsync();
        }

        [Authorize(Roles = AuthRole.ANY)]
        [HttpPost("create")]
        public async Task<ActionResult<Article>> CreateArticle(ArticleViewModel articleVM)
        {
            if (articleVM is null)
                return BadRequest();

            var article = _mapper.Map<Article>(articleVM);

            article.PublishDate = DateTime.Now;

            _db.Attachments.UpdateRange(article.Attachments);
            _db.SaveChanges();

            await _db.Articles.AddAsync(article);
            await _db.SaveChangesAsync();

            return Ok(article);
        }
    }
}