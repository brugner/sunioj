using System.ComponentModel.DataAnnotations;

namespace Sunioj.Core.Resources.Resumes
{
    public class ResumeCourseForUpdateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Institution { get; set; }

        public int? Year { get; set; }
    }
}
