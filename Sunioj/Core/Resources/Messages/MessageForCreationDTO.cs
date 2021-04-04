using System.ComponentModel.DataAnnotations;

namespace Sunioj.Core.Resources.Messages
{
    public class MessageForCreationDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Body { get; set; }
        public int? ServicePackageId { get; set; }
    }
}
