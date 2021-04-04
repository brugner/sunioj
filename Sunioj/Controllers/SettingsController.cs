using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Resources.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Controllers
{
    [ApiController]
    [Route("api/settings")]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        #region Anonymous
        /// <summary>
        /// Get all settings.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SettingDTO>>> GetSettings()
        {
            var settings = await _settingsService.GetAllAsync();

            return Ok(settings);
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
        /// Updates a setting.
        /// </summary>
        /// <param name="settingsForUpdate">Setting data.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<IEnumerable<SettingDTO>>> UpdateSetting([FromBody] IEnumerable<SettingForUpdateDTO> settingsForUpdate)
        {
            var settings = await _settingsService.UpdateAsync(settingsForUpdate);

            return Ok(settings);
        }

        /// <summary>
        /// Updates a setting that is an image.
        /// </summary>
        /// <param name="imageSettingForUpdate">Image setting data.</param>
        /// <returns></returns>
        [HttpPut("image")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<SettingDTO>> UpdateImageSetting([FromForm] ImageSettingForUpdateDTO imageSettingForUpdate)
        {
            var setting = await _settingsService.UpdateImageAsync(imageSettingForUpdate);

            return Ok(setting);
        }
        #endregion
    }
}
