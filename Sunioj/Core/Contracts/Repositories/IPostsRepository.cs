using Sunioj.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.Repositories
{
    public interface IPostsRepository : IRepository<Post, int>
    {
        /// <summary>
        /// Get all published posts.
        /// </summary>
        /// <param name="tag">If specified, it will return only those containing the tag.</param>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetAllPublishedAsync(string tag = null);

        /// <summary>
        /// Get a post by the specified slug.
        /// </summary>
        /// <param name="slug">Slug.</param>
        /// <returns></returns>
        Task<Post> GetBySlugAsync(string slug);

        /// <summary>
        /// Returns true if the specified slug already exists.
        /// </summary>
        /// <param name="slug">Slug.</param>
        /// <returns></returns>
        Task<bool> SlugExists(string slug);

        /// <summary>
        /// Get all tags.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Tag>> GetAllTagsAsync();

        /// <summary>
        /// Returns the tag with the specified name.
        /// </summary>
        /// <param name="name">Tag name.</param>
        /// <returns></returns>
        Task<Tag> GetTagByNameAsync(string name);


        /// <summary>
        /// Returns true if there are any post with the specified tag.
        /// </summary>
        /// <param name="tag">Post tag.</param>
        /// <returns></returns>
        Task<bool> AnyWithTagAsync(string tag);

        /// <summary>
        /// Removes a tag.
        /// </summary>
        /// <param name="tag">Tag.</param>
        void RemoveTag(Tag tag);
    }
}
