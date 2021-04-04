using Sunioj.Core.Resources.ServicePackages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.Services
{
    public interface IServicePackagesService
    {
        /// <summary>
        /// Get all the service packages.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ServicePackageDTO>> GetAllAsync();

        /// <summary>
        /// Creates a new service package.
        /// </summary>
        /// <param name="packageForCreation">Service package data.</param>
        /// <returns></returns>
        Task<ServicePackageDTO> CreateAsync(ServicePackageForCreationDTO packageForCreation);

        /// <summary>
        /// Updates a service package.
        /// </summary>
        /// <param name="id">Service package Id.</param>
        /// <param name="packageForUpdate">Service package data.</param>
        /// <returns></returns>
        Task<ServicePackageDTO> UpdateAsync(int id, ServicePackageForUpdateDTO packageForUpdate);

        /// <summary>
        /// Gets a service package.
        /// </summary>
        /// <param name="id">Service package Id.</param>
        /// <returns></returns>
        Task<ServicePackageDTO> GetByIdAsync(int id);

        /// <summary>
        /// Deletes a service package.
        /// </summary>
        /// <param name="id">Service package Id.</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
