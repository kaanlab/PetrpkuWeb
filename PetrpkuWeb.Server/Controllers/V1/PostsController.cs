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
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Post.ALL)]
        public async Task<ActionResult> GetPosts()
        {
            var posts = await _postService.GetAll();

            return Ok(_mapper.Map<IEnumerable<PostAppUserDepartmentAttachmentsView>>(posts));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Post.DEPARTMENT + "/{departmentId:int}")]
        public async Task<ActionResult> GetPostsByDepartment(int departmentId)
        {
            var posts = await _postService.GetByDepartment(departmentId);

            return Ok(_mapper.Map<IEnumerable<PostAppUserDepartmentAttachmentsView>>(posts));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpPost(ApiRoutes.Post.CREATE)]
        public async Task<ActionResult> CreatePost(PostAppUserDepartmentAttachmentsView postView)
        {
            if (postView is null)
                return NotFound();

            var post = _mapper.Map<Post>(postView);

            if (string.IsNullOrEmpty(postView.Poster))
                post.Poster = "/img/site/default_poster.jpg";

            post.PublishDate = DateTime.Now;

            var created = await _postService.Create(post);
            if (created)
                return Ok(_mapper.Map<PostAppUserDepartmentAttachmentsView>(post));

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Post.SHOW + "/{postId:int}")]
        public async Task<ActionResult> GetPost(int postId)
        {
            var post = await _postService.GetOne(postId);

            if (post is null)
                return NotFound();

            return Ok(_mapper.Map<PostAppUserDepartmentAttachmentsView>(post));
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpPut(ApiRoutes.Post.UPDATE + "/{postViewId:int}")]
        public async Task<ActionResult> UpdatePostAsync(int postViewId, PostView postView)
        {
            var post = await _postService.GetOne(postViewId);

            if (post.PostId == postView.PostId)
            {
                var updatedPost = _mapper.Map(postView, post);
                updatedPost.UpdateDate = DateTime.Now;
                var updated = await _postService.Update(updatedPost);

                if (updated)
                    return Ok(post);
            }
            return BadRequest();
        }

        [Authorize(Roles = AuthRoles.ADMIN_KADRY_USER)]
        [HttpDelete(ApiRoutes.Post.DELETE + "/{postId:int}")]
        public async Task<ActionResult> Delete(int postId)
        {
            if (ModelState.IsValid)
            {
                var deleted = await _postService.Delete(postId);

                if (deleted)
                    return NoContent();
            }
            return BadRequest(ModelState);
        }

    }
}