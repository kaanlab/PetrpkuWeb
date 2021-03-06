using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Shared.Models;
using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Shared.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using PetrpkuWeb.Shared.Extensions;

namespace PetrpkuWeb.Server.Controllers
{
    
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ArticlesController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<ActionResult<List<Article>>> GetArticles()
        {
            return await _db.Articles
                .Include(a => a.Attachments)
                .Include(a => a.Author)
                .Include(ct =>ct.CssType)
                .OrderByDescending(d => d.PublishDate)
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
            var cssType = await _db.CssTypes.SingleOrDefaultAsync(ct => ct.CssTypeId == articleVM.CssTypeId);
            
            article.CssType = cssType;
            article.PublishDate = DateTime.Now;

            _db.Attachments.UpdateRange(article.Attachments);
            _db.SaveChanges();

            await _db.Articles.AddAsync(article);
            await _db.SaveChangesAsync();

            return Ok(article);
        }

        [AllowAnonymous]
        [HttpGet("show/{articleId:int}")]
        public async Task<ActionResult<Article>> GetArticle(int articleId)
        {
            var article = await _db.Articles
                 .Include(a => a.Attachments)
                 .Include(a => a.Author)
                 .Include(ct => ct.CssType)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(u => u.ArticleId == articleId);

            if (article is null)
                return BadRequest();

            return Ok(article);
        }

        [Authorize(Roles = AuthRole.ANY)]
        [HttpPut("update/{articleId:int}")]
        public async Task<ActionResult<Post>> PutUserAsync(int articleId, Article article)
        {
            if (articleId == article.ArticleId)
            {
                var cssType = await _db.CssTypes.SingleOrDefaultAsync(ct => ct.CssTypeId == article.CssTypeId);
                article.CssType = cssType;
                article.UpdateDate = DateTime.Now;
                //_db.Attach(article).State = EntityState.Modified;
                _db.Articles.Update(article);
                await _db.SaveChangesAsync();
                return Ok(article);
            }
            return BadRequest();
        }

        [Authorize(Roles = AuthRole.ANY)]
        [HttpDelete("delete/{articleId:int}")]
        public async Task<IActionResult> Delete(int articleId)
        {
            if (ModelState.IsValid)
            {
                var article = await _db.Articles
                 .Include(a => a.Attachments)
                 .Include(a => a.Author)
                 .SingleOrDefaultAsync(u => u.ArticleId == articleId);

                if (article is null)
                {
                    return NotFound();
                }

                _db.Articles.Remove(article);
                await _db.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}