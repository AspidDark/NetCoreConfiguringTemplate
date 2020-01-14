﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TB.Contracts.V1;
using TB.Contracts.V1.Requests;
using TB.Domain;
using TB.Extensions;
using TB.Services;

namespace TB.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Admin,Poster")]
    [Produces("application/json")]
    public class TagsController : Controller
    {
        private readonly IPostService _postService;
       // private readonly IMapper _mapper;

        public TagsController(IPostService postService /*IMapper mapper*/)
        {
            _postService = postService;
           // _mapper = mapper;
        }
        /// <summary>
        /// Returns all the tags in the system
        /// </summary>
        /// <response code="200">Returns all the tags in the system</response>
        [HttpGet(ApiRoutes.Tags.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetAllTagsAsync());
        }


        [HttpGet(ApiRoutes.Tags.Get)]
        public async Task<IActionResult> Get([FromRoute]string tagName)
        {
            var tag = await _postService.GetTagByNameAsync(tagName);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        /// <summary>
        /// Creates a tag in the system
        /// </summary>
        /// <response code="201">Creates a tag in the system</response>
        /// <response code="400">Unable to create the tag due to validation error</response>
        [HttpPost(ApiRoutes.Tags.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            var newTag = new Tag
            {
                Name = request.TagName,
                CreatorId = HttpContext.GetUserId(),
                CreatedOn = DateTime.UtcNow
            };

            var created = await _postService.CreateTagAsync(newTag);
            if (!created)
            {
                return BadRequest( "Unable to create tag");
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Tags.Get.Replace("{tagName}", newTag.Name);
            return Created(locationUri, newTag);
        }

        [HttpDelete(ApiRoutes.Tags.Delete)]
        // [Authorize(Roles ="Admin")]  [Authorize(Roles = "Poster")]   we need bouth of them
        [Authorize(Roles = "Admin, Poster")] //one of them not bouth
        public async Task<IActionResult> Delete([FromRoute] string tagName)
        {
            var deleted = await _postService.DeleteTagAsync(tagName);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
