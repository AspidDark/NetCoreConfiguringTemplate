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

namespace TB.Controllers.V1
{
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
            return Ok(await _postService.GetPostsAsync());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid postId)//postId has to be much as!!  ApiRoutes.Posts.Get (*)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid postId)
        {
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
            var post = new Post
            {
                Id=postId,
                Name =request.Name
            };

            var updated = await _postService.UpdatePostAsync(post);

            if(updated)
            return Ok(post);
            return NotFound();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post
            {
                Name = postRequest.Name
            };
            await _postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            string locatinUri =baseUrl + "/" +ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse {Id=post.Id };
            return Created(locatinUri, response);
        }
    }
}
