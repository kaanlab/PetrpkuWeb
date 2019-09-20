using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
                .Include(a => a.Author)
                .Include(a => a.Attachments)
                .OrderByDescending(d => d.PublishDate)
                .ToListAsync();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Article>> CreateArticle(ArticleViewModel newArticle)
        {
            if (newArticle != null)
            {
                var article = new Article()
                {
                    AppUserId = newArticle.AppUserId,
                    Title = newArticle.Title,
                    Content = newArticle.Content
                };

                _db.Articles.Add(article);
                _db.SaveChanges();

                article.Attachments.AddRange(newArticle.Attachments);
                _db.Articles.Update(article);

                await _db.SaveChangesAsync();
                return Ok(article);
            }

            return BadRequest();
        }

        [HttpGet("article/{articleId:int}")]
        public ActionResult<Article> GetArticle(int articleId)
        {
            var article = _db.Articles
                 .Where(u => u.ArticleId == articleId)
                 .Include(u => u.Author)
                 .Include(a => a.Attachments)
                 .FirstOrDefault();

            return Ok(article);
        }
    }
}