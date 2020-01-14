using Microsoft.AspNetCore.Mvc;
using TB.Domain;
using System.Collections.Generic;
using System;
using TB.Contracts.V1;
using TB.Contracts.V1.Requests;
using TB.Contracts.V1.Responses;
using System.Linq;
using TB.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TB.Extensions;


/// <summary>
/// Nuget :
/// JwtBearerDefaults  -> Microsoft.AspNetCore.Authentication.JwtBearer
/// </summary>
namespace TB.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        // [Route]
        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetPostsAsync();
            var postResponses = posts.Select(post =>
                new PostResponse
                {
                    Id = post.Id,
                    Name = post.Name,
                    UserId=post.UserId,
                    Tags = post.Tags.Select(x => new TagResponse { Name = x.TagName }).ToList()
                }).ToList();
            return Ok(postResponses);
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid postId)//postId has to be much as!!  ApiRoutes.Posts.Get (*)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            var postResponse = new PostResponse
            {
                Id = post.Id,
                Name = post.Name,
                UserId = post.UserId,
                Tags = post.Tags.Select(x => new TagResponse { Name = x.TagName })
            };
            return Ok(postResponse);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid postId)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }

            var deleted = await _postService.DeletePostAsync(postId);
            if (deleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid postId, [FromBody]UpdatePostRequest request)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }

            var post = await _postService.GetPostByIdAsync(postId);

            post.Name = request.Name;

            var updated = await _postService.UpdatePostAsync(post);

            if (updated)
            {
                var postResponse = new PostResponse
                {
                    Id = post.Id,
                    Name = post.Name,
                    UserId = post.UserId,
                    Tags = post.Tags.Select(x => new TagResponse { Name = x.TagName })
                };
                return Ok(postResponse);

            }
            return NotFound();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post
            {
                Name = postRequest.Name,
                UserId = HttpContext.GetUserId()
            };
            await _postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            string locatinUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse
            {
                Id = post.Id,
                Name = post.Name,
                UserId = post.UserId,
                Tags = post.Tags.Select(x => new TagResponse { Name = x.TagName })
            };
            return Created(locatinUri, response);
        }
    }
}
