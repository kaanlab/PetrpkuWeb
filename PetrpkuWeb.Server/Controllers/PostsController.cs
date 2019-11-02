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
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public PostsController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            return await _db.Posts
                .Include(a => a.Attachments)
                .Include(a => a.Author)
                .Include(d => d.Department)
                .OrderByDescending(d => d.PublishDate)
                .AsNoTracking()
                .ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("bydepartment/{departmentId:int}")]
        public async Task<ActionResult<List<Post>>> GetPostsByDepartment(int departmentId)
        {
            return await _db.Posts
                .Include(a => a.Attachments)
                .Include(a => a.Author)
                .Include(d => d.Department)                
                    .Where(d => d.Department.DepartmentId == departmentId)
                .OrderByDescending(d => d.PublishDate)
                .AsNoTracking()
                .ToListAsync();
        }

        [Authorize(Roles = AuthRole.ANY)]
        [HttpPost("create")]
        public async Task<ActionResult<Post>> CreateArticle(PostViewModel postVM)
        {
            if (postVM is null)
                return BadRequest();

            if(string.IsNullOrEmpty(postVM.Poster))
                postVM.Poster = "/img/site/default_poster.jpg";

            var post = _mapper.Map<Post>(postVM);

            post.PublishDate = DateTime.Now;

            _db.Attachments.UpdateRange(post.Attachments);
            _db.SaveChanges();

            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();

            return Ok(post);
        }

        [AllowAnonymous]
        [HttpGet("show/{postId:int}")]
        public async Task<ActionResult<Post>> GetPost(int postId)
        {
            var post = await _db.Posts
                 .Include(a => a.Attachments)
                 .Include(a => a.Author)
                 .Include(d => d.Department)
                 .AsNoTracking()
                 .SingleOrDefaultAsync(p => p.PostId == postId);

            if (post is null)
                return BadRequest();

            return Ok(post);
        }

        [Authorize(Roles = AuthRole.ANY)]
        [HttpPut("update/{postId:int}")]
        public async Task<ActionResult<Post>> PutUserAsync(int postId, Post post)
        {
            if (postId == post.PostId)
            {
                post.UpdateDate = DateTime.Now;
                //_db.Attach(article).State = EntityState.Modified;
                _db.Posts.Update(post);
                await _db.SaveChangesAsync();
                return Ok(post);
            }
            return BadRequest();
        }

        [Authorize(Roles = AuthRole.ANY)]
        [HttpDelete("delete/{postId:int}")]
        public async Task<IActionResult> Delete(int postId)
        {
            if (ModelState.IsValid)
            {
                var post = await _db.Posts
                 .Include(a => a.Attachments)
                 .Include(a => a.Author)
                 .Include(d => d.Department)
                 .SingleOrDefaultAsync(p => p.PostId == postId);

                if (post is null)
                {
                    return NotFound();
                }

                _db.Posts.Remove(post);
                await _db.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest(ModelState);
        }
    
}
}