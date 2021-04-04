using Sunioj.Data.Entities;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.Repositories
{
    public interface ISettingsRepository : IRepository<Setting, int>
    {
        /// <summary>
        /// Get a setting by name.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <returns></returns>
        Task<Setting> GetByNameAsync(string name);
    }
}
