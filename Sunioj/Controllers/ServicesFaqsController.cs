using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Resources.ServiceFaqs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Controllers
{
    [ApiController]
    [Route("api/service-faqs")]
    [Authorize]
    public class ServicesFaqsController : ControllerBase
    {
        private readonly IServiceFaqsService _servicesFaqsService;

        public ServicesFaqsController(IServiceFaqsService servicesFaqsService)
        {
            _servicesFaqsService = servicesFaqsService;
        }

        #region Anonymous
        /// <summary>
        /// Get all the service frequently asked questions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ServiceFaqDTO>>> GetServiceFaqs()
        {
            var faqs = await _servicesFaqsService.GetAllAsync();

            return Ok(faqs);
        }

        /// <summary>
        /// Returns the allowed HTTP verbs.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetHttpOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,DELETE");
            return Ok();
        }
        #endregion

        #region Authorization
        /// <summary>
        /// Updates a service frequently asked question.
        /// </summary>
        /// <param name="faqForCreation">FAQ data.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ServiceFaqDTO>> CreateFAQ(ServiceFaqForCreationDTO faqForCreation)
        {
            var faq = await _servicesFaqsService.CreateAsync(faqForCreation);

            return Ok(faq);
        }

        /// <summary>
        /// Deletes a service frequently asked question.
        /// </summary>
        /// <param name="id">Service FAQ Id.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaq(int id)
        {
            await _servicesFaqsService.DeleteAsync(id);

            return NoContent();
        }
        #endregion
    }
}
