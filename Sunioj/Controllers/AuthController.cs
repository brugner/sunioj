using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Resources.Auth;
using Sunioj.Extensions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sunioj.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ClaimsPrincipal _authUser;

        public AuthController(IAuthService authService, ClaimsPrincipal authUser)
        {
            _authService = authService;
            _authUser = authUser;
        }

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="userForAuth">User login data.</param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResultDTO>> Login([FromBody] UserForAuthDTO userForAuth)
        {
            var result = await _authService.LoginAsync(userForAuth);

            return Ok(result);
        }

        /// <summary>
        /// Change the password of the authenticated user.
        /// </summary>
        /// <param name="changePassword">Password info.</param>
        /// <returns></returns>
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePassword)
        {
            var userId = _authUser.GetId();
            await _authService.ChangePasswordAsync(changePassword, userId);

            return NoContent();
        }

        /// <summary>
        /// Returns the allowed HTTP verbs.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetHttpOptions()
        {
            Response.Headers.Add("Allow", "POST");
            return Ok();
        }
    }
}
