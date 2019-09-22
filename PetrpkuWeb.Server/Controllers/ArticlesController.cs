using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Shared.Models;
using Microsoft.EntityFrameworkCore;
using PetrpkuWeb.Shared.ViewModels;

namespace PetrpkuWeb.Server.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ArticlesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Article>>> GetArticles()
        {
            return await _db.Articles
                .Include(a => a.Attachments)
                .Include(a => a.Author)
                .ThenInclude(a => a.Avatar)
                .OrderByDescending(d => d.PublishDate)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Article>> CreateArticle(ArticleViewModel newArticle)
        {
            if (newArticle == null)
                return BadRequest();

            var article = new Article()
            {
                AppUserId = newArticle.AppUserId,
                Title = newArticle.Title,
                Content = newArticle.Content,
                PublishDate = DateTime.Now
            };

            _db.Articles.Add(article);
            _db.SaveChanges();

            //article.Attachments = new List<Attachment>();
            article.Attachments.AddRange(newArticle.Attachments);
            _db.Articles.Update(article);

            await _db.SaveChangesAsync();

            return Ok(article);
        }

        [HttpGet("show/{articleId:int}")]
        public async Task<ActionResult<Article>> GetArticle(int articleId)
        {
            var article = await _db.Articles
                 .Include(a => a.Attachments)
                 .Include(a => a.Author)
                 .ThenInclude(a => a.Avatar)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(u => u.ArticleId == articleId);

            if (article == null)
                return BadRequest();

            return Ok(article);
        }

        [HttpPut("update/{articleId:int}")]
        public async Task<IActionResult> PutUserAsync(int articleId, Article article)
        {
            
            article.PublishDate = DateTime.Now;
            //_db.Attach(article).State = EntityState.Modified;
            _db.Update(article);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete/{articleId:int}")]
        public async Task<IActionResult> Delete(int articleId)
        {
            if (ModelState.IsValid)
            {
                var article = await _db.Articles
                 .Include(a => a.Attachments)
                 .Include(a => a.Author)
                 .ThenInclude(a => a.Avatar)
                 .SingleOrDefaultAsync(u => u.ArticleId == articleId);

                if (article == null)
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