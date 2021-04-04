using System.Collections.Generic;

namespace Sunioj.Core.Resources.Resumes
{
    public class ResumeDTO
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }
        public ICollection<ResumeExperienceDTO> Experience { get; set; }
        public ICollection<ResumeEducationDTO> Education { get; set; }
        public ICollection<ResumeCourseDTO> Courses { get; set; }
        public ICollection<ResumeSkillDTO> Skills { get; set; }
        public ICollection<ResumeLanguageDTO> Languages { get; set; }
        public ICollection<ResumeInterestDTO> Interests { get; set; }
    }
}
