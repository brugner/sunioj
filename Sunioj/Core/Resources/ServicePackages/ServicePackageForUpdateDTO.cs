using System.ComponentModel.DataAnnotations;

namespace Sunioj.Core.Resources.ServicePackages
{
    public class ServicePackageForUpdateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Order { get; set; }
    }
}
