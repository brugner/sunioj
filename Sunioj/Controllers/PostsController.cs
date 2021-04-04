using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Resources.Posts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Controllers
{
    [ApiController]
    [Route("api/posts")]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        #region Anonymous
        /// <summary>
        /// Get all published posts.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublishedPostDTO>>> GetPublishedPosts()
        {
            var posts = await _postsService.GetAllPublishedAsync();

            return Ok(posts);
        }

        /// <summary>
        /// Get all posts with the specified tag.
        /// </summary>
        /// <param name="tag">Post tag.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("tag/{tag}")]
        public async Task<ActionResult<IEnumerable<PublishedPostDTO>>> GetPublishedPostsWithTag(string tag)
        {
            var posts = await _postsService.GetAllPublishedAsync(tag);

            return Ok(posts);
        }

        /// <summary>
        /// Get a post with the specified slug.
        /// </summary>
        /// <param name="slug">Slug.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<PublishedPostDTO>> GetPostBySlug(string slug)
        {
            var post = await _postsService.GetBySlugAsync(slug);

            return Ok(post);
        }

        /// <summary>
        /// Get all the tags.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("tags")]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetTags()
        {
            var tags = await _postsService.GetAllTagsAsync();

            return Ok(tags);
        }

        /// <summary>
        /// Returns the allowed HTTP verbs.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetHttpOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,PUT,DELETE");
            return Ok();
        }
        #endregion

        #region Authorization
        /// <summary>
        /// Get the post with the specified Id.
        /// </summary>
        /// <param name="id">Post Id.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetPost")]
        public async Task<ActionResult<PostDTO>> GetPostById(int id)
        {
            var post = await _postsService.GetByIdAsync(id);

            return Ok(post);
        }

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="postForCreation">Post data.</param>
        /// <returns></returns>
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<PublishedPostDTO>> Create([FromForm] PostForCreationDTO postForCreation)
        {
            var post = await _postsService.CreateAsync(postForCreation);

            return CreatedAtRoute("GetPost", new { post.Id }, post);
        }

        /// <summary>
        /// Updates the specified post.
        /// </summary>
        /// <param name="id">Post Id.</param>
        /// <param name="postForUpdate">Post data.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<PublishedPostDTO>> Update(int id, [FromForm] PostForUpdateDTO postForUpdate)
        {
            var post = await _postsService.UpdateAsync(id, postForUpdate);

            return Ok(post);
        }

        /// <summary>
        /// Deletes the specified post.
        /// </summary>
        /// <param name="id">Post Id.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _postsService.DeleteAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Get all posts including those marked as draft.
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts()
        {
            var posts = await _postsService.GetAllAsync();

            return Ok(posts);
        }
        #endregion
    }
}
