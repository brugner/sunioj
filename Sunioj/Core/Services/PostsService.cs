using AutoMapper;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Contracts.UnitsOfWork;
using Sunioj.Core.Exceptions;
using Sunioj.Core.Resources.Posts;
using Sunioj.Data.Entities;
using Sunioj.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Core.Services
{
    public class PostsService : IPostsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilesService _fileService;

        public PostsService(IMapper mapper, IUnitOfWork unitOfWork, IFilesService fileService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<IEnumerable<PublishedPostDTO>> GetAllPublishedAsync(string tag = null)
        {
            var posts = await _unitOfWork.Posts.GetAllPublishedAsync(tag);

            return _mapper.Map<IEnumerable<PublishedPostDTO>>(posts);
        }

        public async Task<IEnumerable<PostDTO>> GetAllAsync()
        {
            var posts = await _unitOfWork.Posts.GetAllAsync();

            return _mapper.Map<IEnumerable<PostDTO>>(posts);
        }

        public async Task<PublishedPostDTO> GetBySlugAsync(string slug)
        {
            var post = await _unitOfWork.Posts.GetBySlugAsync(slug);

            if (post == null)
            {
                throw new NotFoundException($"Post {slug} not found");
            }

            return _mapper.Map<PublishedPostDTO>(post);
        }

        public async Task<PostDTO> GetByIdAsync(int id)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);

            if (post == null)
            {
                throw new NotFoundException($"Post {id} not found");
            }

            return _mapper.Map<PostDTO>(post);
        }

        public async Task<PublishedPostDTO> CreateAsync(PostForCreationDTO postForCreation)
        {
            var post = _mapper.Map<Post>(postForCreation);
            post.Slug = await SetSlugAsync(post.Title);
            post.Tags = await SetTagsAsync(postForCreation.Tags);
            post.CreatedAt = DateTime.Now;

            _unitOfWork.Posts.Add(post);
            await _unitOfWork.CompleteAsync();

            post.Thumbnail = await _fileService.SavePostThumbnailAsync(post.Id, postForCreation.Thumbnail);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<PublishedPostDTO>(post);
        }

        public async Task<PublishedPostDTO> UpdateAsync(int id, PostForUpdateDTO postForUpdate)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);

            if (post == null)
            {
                throw new NotFoundException($"Post {id} not found");
            }

            _mapper.Map(postForUpdate, post);

            post.Slug = await SetSlugAsync(post.Title);
            post.UpdatedAt = DateTime.Now;
            post.Tags = await SetTagsAsync(postForUpdate.Tags);
            post.Thumbnail = await _fileService.SavePostThumbnailAsync(post.Id, postForUpdate.Thumbnail);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<PublishedPostDTO>(post);
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);

            if (post == null)
            {
                throw new NotFoundException($"Post {id} not found");
            }

            _unitOfWork.Posts.Remove(post);
            await _unitOfWork.CompleteAsync();

            await DeleteOrphanTags(post.Tags);
            _fileService.DeletePostThumbnail(id);
        }

        public async Task<IEnumerable<TagDTO>> GetAllTagsAsync()
        {
            var tags = await _unitOfWork.Posts.GetAllTagsAsync();

            return _mapper.Map<IEnumerable<TagDTO>>(tags);
        }

        #region Helpers
        private async Task<string> SetSlugAsync(string title)
        {
            string slug = title.ToSlug();

            if (await _unitOfWork.Posts.SlugExists(slug))
            {
                slug += "-" + DateTime.Now.Millisecond;
            }

            return slug;
        }

        private async Task<ICollection<Tag>> SetTagsAsync(ICollection<TagDTO> tags)
        {
            var result = new List<Tag>();

            foreach (var tag in tags)
            {
                var tagEntity = await _unitOfWork.Posts.GetTagByNameAsync(tag.Name);

                if (tagEntity != null)
                {
                    result.Add(tagEntity);
                }
                else
                {
                    result.Add(new Tag(tag.Name));
                }
            }

            return result;
        }

        /// <summary>
        /// If a tag is no loger associated to any post, it will be deleted.
        /// </summary>
        /// <param name="tags">Post tags.</param>
        /// <returns></returns>
        private async Task DeleteOrphanTags(ICollection<Tag> tags)
        {
            foreach (var tag in tags)
            {
                var anyPostWithTag = await _unitOfWork.Posts.AnyWithTagAsync(tag.Name);

                if (!anyPostWithTag)
                {
                    _unitOfWork.Posts.RemoveTag(tag);
                }
            }

            await _unitOfWork.CompleteAsync();
        }
        #endregion
    }
}
