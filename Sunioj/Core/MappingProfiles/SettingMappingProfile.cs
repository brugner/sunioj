using AutoMapper;
using Sunioj.Core.Resources.Settings;
using Sunioj.Data.Entities;

namespace Sunioj.Core.MappingProfiles
{
    public class SettingMappingProfile : Profile
    {
        public SettingMappingProfile()
        {
            CreateMap<Setting, SettingDTO>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom((src, dest) =>
                {
                    if (src.Name.Equals("site.image") && string.IsNullOrEmpty(src.Value))
                    {
                        return "images\\settings\\site.image-default.jpg";
                    }
                    else if (src.Name.Equals("about-me.image") && string.IsNullOrEmpty(src.Value))
                    {
                        return "images\\settings\\about-me.image-default.jpg";
                    }
                    else
                    {
                        return src.Value;
                    }
                }));
        }
    }
}
