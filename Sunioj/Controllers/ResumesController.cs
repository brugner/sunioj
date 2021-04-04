using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Resources.Resumes;
using System.Threading.Tasks;

namespace Sunioj.Controllers
{
    [ApiController]
    [Route("api/resumes")]
    [Authorize]
    public class ResumesController : ControllerBase
    {
        private readonly IResumesService _resumesService;

        public ResumesController(IResumesService resumesService)
        {
            _resumesService = resumesService;
        }

        #region Anonymous
        /// <summary>
        /// Gets the editor's resume.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResumeDTO>> GetResume()
        {
            var resume = await _resumesService.GetAsync();

            return Ok(resume);
        }

        /// <summary>
        /// Returns the allowed HTTP verbs.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetHttpOptions()
        {
            Response.Headers.Add("Allow", "GET,PUT");
            return Ok();
        }
        #endregion

        #region Authorization
        /// <summary>
        /// Updates the editor's resume.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ResumeDTO>> UpdateResume([FromBody] ResumeForUpdateDTO resumeForUpdate)
        {
            var resume = await _resumesService.UpdateAsync(resumeForUpdate);

            return Ok(resume);
        }
        #endregion
    }
}
