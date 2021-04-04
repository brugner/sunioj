using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Resources.ServicePackages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Controllers
{
    [ApiController]
    [Route("api/service-packages")]
    [Authorize]
    public class ServicePackagesController : ControllerBase
    {
        private readonly IServicePackagesService _servicePackagesService;

        public ServicePackagesController(IServicePackagesService servicePackagesService)
        {
            _servicePackagesService = servicePackagesService;
        }

        #region Anonymous
        /// <summary>
        /// Get all the service packages.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ServicePackageDTO>>> GetServicePackages()
        {
            var packages = await _servicePackagesService.GetAllAsync();

            return Ok(packages);
        }

        /// <summary>
        /// Returns the allowed HTTP verbs.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetHttpOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,PUT,DELETE");
            return Ok();
        }
        #endregion

        #region Authorization
        /// <summary>
        /// Creates a new service package.
        /// </summary>
        /// <param name="packageForCreation">Service package data.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ServicePackageDTO>> CreatePackage(ServicePackageForCreationDTO packageForCreation)
        {
            var package = await _servicePackagesService.CreateAsync(packageForCreation);

            return Ok(package);
        }

        /// <summary>
        /// Updates a service package.
        /// </summary>
        /// <param name="packageForUpdate">Service package data.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ServicePackageDTO>> UpdatePackage(int id, [FromBody] ServicePackageForUpdateDTO packageForUpdate)
        {
            var package = await _servicePackagesService.UpdateAsync(id, packageForUpdate);

            return Ok(package);
        }

        /// <summary>
        /// Get a service package.
        /// </summary>
        /// <param name="id">Service package Id.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicePackageDTO>> GetPackage(int id)
        {
            var package = await _servicePackagesService.GetByIdAsync(id);

            return Ok(package);
        }

        /// <summary>
        /// Deletes a service package.
        /// </summary>
        /// <param name="id">Service package Id.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _servicePackagesService.DeleteAsync(id);

            return NoContent();
        }
        #endregion
    }
}
