using AutoMapper;
using Sunioj.Core.Resources.ServiceFaqs;
using Sunioj.Data.Entities;

namespace Sunioj.Core.MappingProfiles
{
    public class ServiceFaqMappingProfile : Profile
    {
        public ServiceFaqMappingProfile()
        {
            CreateMap<ServiceFaq, ServiceFaqDTO>();
            CreateMap<ServiceFaqForCreationDTO, ServiceFaq>();
        }
    }
}
