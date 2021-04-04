using AutoMapper;
using Sunioj.Core.Resources.Posts;
using Sunioj.Data.Entities;

namespace Sunioj.Core.MappingProfiles
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Post, PublishedPostDTO>()
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom((src, dest) =>
                {
                    return string.IsNullOrEmpty(src.Thumbnail) ? "images\\posts\\default-thumbnail.jpg" : src.Thumbnail;
                }));

            CreateMap<Post, PostDTO>()
            .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom((src, dest) =>
             {
                 return string.IsNullOrEmpty(src.Thumbnail) ? "images\\posts\\default-thumbnail.jpg" : src.Thumbnail;
             }));

            CreateMap<PostForCreationDTO, Post>();

            CreateMap<PostForUpdateDTO, Post>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
