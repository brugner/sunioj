using Sunioj.Core.Resources.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.Services
{
    public interface ISettingsService
    {
        /// <summary>
        /// Updates the settings.
        /// </summary>
        /// <param name="settingsForUpdate">Settings data.</param>
        /// <returns></returns>
        Task<IEnumerable<SettingDTO>> UpdateAsync(IEnumerable<SettingForUpdateDTO> settingsForUpdate);

        /// <summary>
        /// Returns all the settings.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SettingDTO>> GetAllAsync();

        /// <summary>
        /// Updates a setting that is an image.
        /// </summary>
        /// <param name="imageSettingForUpdate">Image setting data/</param>
        /// <returns></returns>
        Task<SettingDTO> UpdateImageAsync(ImageSettingForUpdateDTO imageSettingForUpdate);
    }
}
