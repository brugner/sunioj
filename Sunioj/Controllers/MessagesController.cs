using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Resources.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Controllers
{
    [ApiController]
    [Route("api/messages")]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService _messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        #region Anonymous
        /// <summary>
        /// Creates a new message.
        /// </summary>
        /// <param name="messageForCreation">Message data.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<MessageDTO>> Create(MessageForCreationDTO messageForCreation)
        {
            var message = await _messagesService.CreateAsync(messageForCreation);

            return Ok(message);
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
        /// Get all messages.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessages()
        {
            var messages = await _messagesService.GetAllAsync();

            return Ok(messages);
        }

        /// <summary>
        /// Get the message with the specified Id.
        /// </summary>
        /// <param name="id">Message Id.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDTO>> GetMessage(int id)
        {
            var message = await _messagesService.GetByIdAsync(id);

            return Ok(message);
        }

        /// <summary>
        /// Deletes the specified message.
        /// </summary>
        /// <param name="id">Message Id.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _messagesService.DeleteAsync(id);

            return NoContent();
        }
        #endregion
    }
}
