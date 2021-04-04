using AutoMapper;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Contracts.UnitsOfWork;
using Sunioj.Core.Resources.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunioj.Core.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilesService _filesService;

        public SettingsService(IMapper mapper, IUnitOfWork unitOfWork, IFilesService filesService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _filesService = filesService;
        }

        public async Task<IEnumerable<SettingDTO>> GetAllAsync()
        {
            var settings = await _unitOfWork.Settings.GetAllAsync();

            return _mapper.Map<IEnumerable<SettingDTO>>(settings);
        }

        public async Task<IEnumerable<SettingDTO>> UpdateAsync(IEnumerable<SettingForUpdateDTO> settingsForUpdate)
        {
            var settings = await _unitOfWork.Settings.GetAllAsync();

            foreach (var setting in settings)
            {
                if (settingsForUpdate.Any(x => x.Name.ToLower().Equals(setting.Name.ToLower())))
                {
                    setting.Value = settingsForUpdate.First(x => x.Name.ToLower().Equals(setting.Name.ToLower())).Value;
                    setting.UpdatedAt = DateTime.Now;
                }
            }

            await _unitOfWork.CompleteAsync();

            return await GetAllAsync();
        }

        public async Task<SettingDTO> UpdateImageAsync(ImageSettingForUpdateDTO imageSettingForUpdate)
        {
            var setting = await _unitOfWork.Settings.GetByNameAsync(imageSettingForUpdate.Name);
            var path = await _filesService.SaveImageSettingAsync(imageSettingForUpdate.Name, imageSettingForUpdate.Image);

            setting.Value = path;
            setting.UpdatedAt = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SettingDTO>(setting);
        }
    }
}
