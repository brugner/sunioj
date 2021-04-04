using Sunioj.Core.Resources.Resumes;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.Services
{
    public interface IResumesService
    {
        /// <summary>
        /// Gets the editor's resume.
        /// </summary>
        /// <returns></returns>
        Task<ResumeDTO> GetAsync();

        /// <summary>
        /// Updates the editor's resume.
        /// </summary>
        /// <returns></returns>
        Task<ResumeDTO> UpdateAsync(ResumeForUpdateDTO resumeForUpdate);
    }
}
