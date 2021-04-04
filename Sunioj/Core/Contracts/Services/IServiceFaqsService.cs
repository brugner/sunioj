using Sunioj.Core.Resources.ServiceFaqs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.Services
{
    public interface IServiceFaqsService
    {
        /// <summary>
        /// Get all the service frequently asked questions.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ServiceFaqDTO>> GetAllAsync();

        /// <summary>
        /// Creates a new service frequently asked question.
        /// </summary>
        /// <param name="faqForCreation">Service FAQ data.</param>
        /// <returns></returns>
        Task<ServiceFaqDTO> CreateAsync(ServiceFaqForCreationDTO faqForCreation);

        /// <summary>
        /// Deletes a service frequently asked question.
        /// </summary>
        /// <param name="id">Faq Id.</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
