
using BloggingApplicationAPI.Models;
using BloggingApplicationAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllPosts()
        //{
        //    var posts = await _postService.GetAllPostsAsync();
        //    return Ok(posts);
        //}

        [HttpGet("Pagination")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound("Post not found");
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdPost = await _postService.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedPost = await _postService.UpdatePostAsync(id, post);
            if (updatedPost == null) return NotFound("Post not found");
            return Ok(updatedPost);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var success = await _postService.DeletePostAsync(id);
            if (!success) return NotFound("Post not found");
            return NoContent();
        }
    }
}
