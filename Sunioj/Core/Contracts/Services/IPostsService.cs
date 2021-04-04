using Sunioj.Core.Resources.Posts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.Services
{
    public interface IPostsService
    {
        /// <summary>
        /// Get all published posts.
        /// </summary>
        /// <param name="tag">If specified, it will return only those containing the tag.</param>
        /// <returns></returns>
        Task<IEnumerable<PublishedPostDTO>> GetAllPublishedAsync(string tag = null);

        /// <summary>
        /// Get all posts including those marked as draft.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PostDTO>> GetAllAsync();

        /// <summary>
        /// Get a post with the specified slug.
        /// </summary>
        /// <param name="slug">Slug.</param>
        /// <returns></returns>
        Task<PublishedPostDTO> GetBySlugAsync(string slug);

        /// <summary>
        /// Get a post.
        /// </summary>
        /// <param name="id">Post Id.</param>
        /// <returns></returns>
        Task<PostDTO> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="postForCreation">Post data.</param>
        /// <returns></returns>
        Task<PublishedPostDTO> CreateAsync(PostForCreationDTO postForCreation);

        /// <summary>
        /// Updates the specified post.
        /// </summary>
        /// <param name="id">Post Id.</param>
        /// <param name="postForUpdate">Post data.</param>
        /// <returns></returns>
        Task<PublishedPostDTO> UpdateAsync(int id, PostForUpdateDTO postForUpdate);

        /// <summary>
        /// Deletes the specified post.
        /// </summary>
        /// <param name="id">Post Id.</param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Get all tags.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TagDTO>> GetAllTagsAsync();
    }
}
