using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sunioj.Core.Resources.Resumes
{
    public class ResumeForUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(600)]
        public string Summary { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Website { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        public ICollection<ResumeExperienceForUpdateDTO> Experience { get; set; }
        public ICollection<ResumeEducationForUpdateDTO> Education { get; set; }
        public ICollection<ResumeCourseForUpdateDTO> Courses { get; set; }
        public ICollection<ResumeSkillForUpdateDTO> Skills { get; set; }
        public ICollection<ResumeLanguageForUpdateDTO> Languages { get; set; }
        public ICollection<ResumeInterestForUpdateDTO> Interests { get; set; }
    }
}
