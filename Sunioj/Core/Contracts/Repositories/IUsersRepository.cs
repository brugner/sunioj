using Sunioj.Data.Entities;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.Repositories
{
    public interface IUsersRepository : IRepository<User, int>
    {
        /// <summary>
        /// Get a user with the specified email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns></returns>
        Task<User> GetByEmailAsync(string email);
    }
}
