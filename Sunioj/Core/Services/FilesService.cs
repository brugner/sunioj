using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sunioj.Core.Contracts.Services;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Sunioj.Core.Services
{
    public class FilesService : IFilesService
    {
        private readonly string _postsFolder = Path.Combine("images", "posts");
        private readonly string _settingsFolder = Path.Combine("images", "settings");
        private const string THUMBNAIL = "thumbnail";
        private readonly ILogger<FilesService> _logger;

        public FilesService(ILogger<FilesService> logger)
        {
            _logger = logger;
        }

        public async Task<string> SavePostThumbnailAsync(int postId, IFormFile thumbnail)
        {
            try
            {
                if (thumbnail == null || thumbnail.Length == 0)
                {
                    return null;
                }

                var pathToFolder = GetPathToPostsFolder(postId);
                var fileName = ContentDispositionHeaderValue.Parse(thumbnail.ContentDisposition).FileName.Trim('"');

                fileName = THUMBNAIL + Path.GetExtension(fileName);

                var pathToFile = Path.Combine(pathToFolder, fileName);

                DeletePostThumbnail(postId);

                if (!Directory.Exists(pathToFolder))
                {
                    Directory.CreateDirectory(pathToFolder);
                }

                using var stream = new FileStream(pathToFile, FileMode.Create);
                await thumbnail.CopyToAsync(stream);

                return Path.Combine(_postsFolder, postId.ToString(), fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public void DeletePostThumbnail(int postId)
        {
            var pathToFolder = GetPathToPostsFolder(postId);

            if (!Directory.Exists(pathToFolder))
            {
                return;
            }

            var existingFiles = Directory.GetFiles(pathToFolder, $"{THUMBNAIL}.*");

            if (existingFiles.Length > 0)
            {
                foreach (var file in existingFiles)
                {
                    File.Delete(file);
                }
            }

            Directory.Delete(pathToFolder);
        }

        public async Task<string> SaveImageSettingAsync(string settingName, IFormFile settingImage)
        {
            try
            {
                if (settingImage == null || settingImage.Length == 0)
                {
                    return null;
                }

                var pathToFolder = GetPathToSettingsFolder();
                var fileName = ContentDispositionHeaderValue.Parse(settingImage.ContentDisposition).FileName.Trim('"');
                fileName = settingName + Path.GetExtension(fileName);

                var pathToFile = Path.Combine(pathToFolder, fileName);

                DeleteSettingImage(settingName);

                if (!Directory.Exists(pathToFolder))
                {
                    Directory.CreateDirectory(pathToFolder);
                }

                using var stream = new FileStream(pathToFile, FileMode.Create);
                await settingImage.CopyToAsync(stream);

                return Path.Combine(_settingsFolder, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        private string GetPathToPostsFolder(int postId)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _postsFolder, postId.ToString());
        }

        private string GetPathToSettingsFolder()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _settingsFolder);
        }

        private void DeleteSettingImage(string settingName)
        {
            var pathToFolder = GetPathToSettingsFolder();

            if (!Directory.Exists(pathToFolder))
            {
                return;
            }

            var existingFiles = Directory.GetFiles(pathToFolder, $"{settingName}.*");

            if (existingFiles.Length > 0)
            {
                foreach (var file in existingFiles)
                {
                    File.Delete(file);
                }
            }
        }
    }
}
