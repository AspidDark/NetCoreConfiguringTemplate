using Microsoft.AspNetCore.Mvc;
using TB.Domain;
using System.Collections.Generic;
using System;
using TB.Contracts.V1;

namespace TB.Controllers.V1
{
    public class PostsController : Controller
    {
        private List<Post> _posts;
        public PostsController()
        {
            _posts = new List<Post>();
            for (int i = 0; i < 5; i++)
            {
                _posts.Add(new Post { Id = Guid.NewGuid().ToString() });
            }
        }
       // [Route]
        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_posts);
        }
    }
}
