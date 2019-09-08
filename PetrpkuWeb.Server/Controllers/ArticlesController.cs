using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Shared.Models;
using Microsoft.EntityFrameworkCore;

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
                .OrderBy(d => d.PublishDate)
                .ToListAsync();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Article>> CreateArticle(Article article)
        {
            _db.Articles.Add(article);
            await _db.SaveChangesAsync();
            return Ok();
        }


    }
}