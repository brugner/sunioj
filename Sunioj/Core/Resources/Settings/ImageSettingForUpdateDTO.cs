using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Sunioj.Core.Resources.Settings
{
    public class ImageSettingForUpdateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
