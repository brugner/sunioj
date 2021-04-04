using AutoMapper;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Contracts.UnitsOfWork;
using Sunioj.Core.Exceptions;
using Sunioj.Core.Resources.ServicePackages;
using Sunioj.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Core.Services
{
    public class ServicePackagesService : IServicePackagesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ServicePackagesService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServicePackageDTO> CreateAsync(ServicePackageForCreationDTO packageForCreation)
        {
            var package = _mapper.Map<ServicePackage>(packageForCreation);
            package.CreatedAt = DateTime.Now;

            _unitOfWork.ServicePackages.Add(package);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ServicePackageDTO>(package);
        }

        public async Task DeleteAsync(int id)
        {
            var package = await _unitOfWork.ServicePackages.GetByIdAsync(id);

            if (package == null)
            {
                throw new NotFoundException($"Service package {id} not found");
            }

            _unitOfWork.ServicePackages.Remove(package);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<ServicePackageDTO>> GetAllAsync()
        {
            var packages = await _unitOfWork.ServicePackages.GetAllAsync();

            return _mapper.Map<IEnumerable<ServicePackageDTO>>(packages);
        }

        public async Task<ServicePackageDTO> GetByIdAsync(int id)
        {
            var package = await _unitOfWork.ServicePackages.GetByIdAsync(id);

            if (package == null)
            {
                throw new NotFoundException($"Service package {id} not found");
            }

            return _mapper.Map<ServicePackageDTO>(package);
        }

        public async Task<ServicePackageDTO> UpdateAsync(int id, ServicePackageForUpdateDTO packageForUpdate)
        {
            var package = await _unitOfWork.ServicePackages.GetByIdAsync(id);

            if (package == null)
            {
                throw new NotFoundException($"Service package {id} not found");
            }

            _mapper.Map(packageForUpdate, package);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ServicePackageDTO>(package);
        }
    }
}
