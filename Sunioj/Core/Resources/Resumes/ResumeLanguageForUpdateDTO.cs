using System.ComponentModel.DataAnnotations;

namespace Sunioj.Core.Resources.Resumes
{
    public class ResumeLanguageForUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Level { get; set; }
    }
}
