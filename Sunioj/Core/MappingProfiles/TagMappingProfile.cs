using AutoMapper;
using Sunioj.Core.Resources.Posts;
using Sunioj.Data.Entities;

namespace Sunioj.API.Core.MappingProfiles
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<Tag, TagDTO>();
            CreateMap<TagDTO, Tag>();
        }
    }
}
