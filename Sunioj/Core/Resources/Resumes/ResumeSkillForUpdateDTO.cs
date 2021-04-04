using System.ComponentModel.DataAnnotations;

namespace Sunioj.Core.Resources.Resumes
{
    public class ResumeSkillForUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
