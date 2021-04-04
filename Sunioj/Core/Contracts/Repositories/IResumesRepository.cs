using Sunioj.Data.Entities;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.Repositories
{
    public interface IResumesRepository : IRepository<Resume, int>
    {
        /// <summary>
        /// Gets the editor's resume.
        /// </summary>
        /// <returns></returns>
        Task<Resume> GetAsync();
    }
}
