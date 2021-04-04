using AutoMapper;
using Moq;
using Sunioj.Core.Contracts.Repositories;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Contracts.UnitsOfWork;
using Sunioj.Core.Resources.Posts;
using Sunioj.Core.Services;
using Sunioj.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class PostsServiceTests
    {
        [Fact]
        public async void GetAll_PostsExist_ReturnsPostsDTO()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var postsRepositoryMock = new Mock<IPostsRepository>();
            var mapperMock = new Mock<IMapper>();
            var fileServiceMock = new Mock<IFilesService>();
            var expected = GetPostsDTO();

            unitOfWorkMock.Setup(x => x.Posts).Returns(postsRepositoryMock.Object);
            postsRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(GetPosts());
            mapperMock.Setup(x => x.Map<IEnumerable<PostDTO>>(It.IsAny<IEnumerable<Post>>())).Returns(expected);

            var service = new PostsService(mapperMock.Object, unitOfWorkMock.Object, fileServiceMock.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Count(), result.Count());
            Assert.Equal(expected.First().Title, result.First().Title);
        }

        #region Helpers
        private static IEnumerable<Post> GetPosts()
        {
            return new List<Post>
            {
                new Post
                {
                    Id = 1,
                    Title = "Title 1",
                    Summary = "Summary 1",
                    Content = "Content 1",
                    Slug = "title-1",
                    Tags = new List<Tag> { new Tag("tag1") },
                    CreatedAt = DateTime.Now
                },
                new Post
                {
                    Id = 2,
                    Title = "Title 2",
                    Summary = "Summary 2",
                    Content = "Content 2",
                    Slug = "title-2",
                    Tags = new List<Tag> { new Tag("tag1") },
                    CreatedAt = DateTime.Now
                }
            };
        }

        private static IEnumerable<PostDTO> GetPostsDTO()
        {
            return new List<PostDTO>
            {
                new PostDTO
                {
                    Id = 1,
                    Title = "Title 1",
                    Summary = "Summary 1",
                    Content = "Content 1",
                    Slug = "title-1",
                    Tags = new List<TagDTO> { new TagDTO("tag1") },
                    CreatedAt = DateTime.Now
                },
                new PostDTO
                {
                    Id = 2,
                    Title = "Title 2",
                    Summary = "Summary 2",
                    Content = "Content 2",
                    Slug = "title-2",
                    Tags = new List<TagDTO> { new TagDTO("tag1") },
                    CreatedAt = DateTime.Now
                }
            };
        }
        #endregion
    }
}
