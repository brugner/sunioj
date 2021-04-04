using Sunioj.Core.Resources.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.Services
{
    public interface IMessagesService
    {
        /// <summary>
        /// Creates a new message.
        /// </summary>
        /// <param name="messageForCreation">Message data.</param>
        /// <returns></returns>
        Task<MessageDTO> CreateAsync(MessageForCreationDTO messageForCreation);

        /// <summary>
        /// Get all the messages.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MessageDTO>> GetAllAsync();

        /// <summary>
        /// Get the specified message.
        /// </summary>
        /// <param name="id">Message Id.</param>
        /// <returns></returns>
        Task<MessageDTO> GetByIdAsync(int id);

        /// <summary>
        /// Deletes the specified message.
        /// </summary>
        /// <param name="id">Message Id.</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
