using Microsoft.EntityFrameworkCore;
using Sunioj.Core.Contracts.Repositories;
using Sunioj.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunioj.Data.Repositories
{
    public class PostsRepository : Repository<Post, int>, IPostsRepository
    {
        public PostsRepository(AppDbContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Post>> GetAllAsync()
        {
            var posts = await AppDbContext.Posts
                 .Include(x => x.Tags)
                 .ToListAsync();

            return posts.OrderByDescending(x => x.CreatedAt);
        }

        public async Task<IEnumerable<Post>> GetAllPublishedAsync(string tag = null)
        {
            var postsQuery = AppDbContext.Posts
                .Include(x => x.Tags)
                .AsQueryable();

            if (!string.IsNullOrEmpty(tag))
            {
                tag = tag.ToLower();

                postsQuery = postsQuery
                     .Where(x => x.Tags.Any(x => x.Name.Equals(tag)));
            }

            var posts = await postsQuery
                .Where(x => !x.IsDraft)
                .ToListAsync();

            return posts.OrderByDescending(x => x.CreatedAt);
        }

        public override async ValueTask<Post> GetByIdAsync(int id)
        {
            return await AppDbContext.Posts
                .Include(x => x.Tags)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Post> GetBySlugAsync(string slug)
        {
            return await AppDbContext.Posts
                .Include(x => x.Tags)
                .SingleOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<bool> SlugExists(string slug)
        {
            return await AppDbContext.Posts.FirstOrDefaultAsync(x => x.Slug == slug) != null;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await AppDbContext.Tags
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Tag> GetTagByNameAsync(string name)
        {
            name = name.ToLower();

            return await AppDbContext.Tags.SingleOrDefaultAsync(x => x.Name.Equals(name));
        }

        public async Task<bool> AnyWithTagAsync(string tag)
        {
            var count = await AppDbContext.Posts
                .Include(x => x.Tags)
                .Where(x => x.Tags.Any(x => x.Name.Equals(tag)))
                .CountAsync();

            return count > 0;
        }

        public void RemoveTag(Tag tag)
        {
            AppDbContext.Tags.Remove(tag);
        }
    }
}
