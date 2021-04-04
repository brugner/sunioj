using Sunioj.Core.Resources.Auth;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="userForAuth">User login data.</param>
        /// <returns></returns>
        Task<AuthResultDTO> LoginAsync(UserForAuthDTO userForAuth);

        /// <summary>
        /// Change the password for the specified user.
        /// </summary>
        /// <param name="changePassword">Password data.</param>
        /// <param name="userId">User Id.</param>
        /// <returns></returns>
        Task ChangePasswordAsync(ChangePasswordDTO changePassword, int userId);
    }
}
