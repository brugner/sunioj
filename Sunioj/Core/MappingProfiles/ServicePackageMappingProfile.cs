using AutoMapper;
using Sunioj.Core.Resources.ServicePackages;
using Sunioj.Data.Entities;

namespace Sunioj.Core.MappingProfiles
{
    public class ServicePackageMappingProfile : Profile
    {
        public ServicePackageMappingProfile()
        {
            CreateMap<ServicePackage, ServicePackageDTO>();
            CreateMap<ServicePackageForCreationDTO, ServicePackage>();

            CreateMap<ServicePackageForUpdateDTO, ServicePackage>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
