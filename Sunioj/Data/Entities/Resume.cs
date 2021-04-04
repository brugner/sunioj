using System.Collections.Generic;

namespace Sunioj.Data.Entities
{
    public class Resume : Entity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }
        public ICollection<ResumeExperience> Experience { get; set; }
        public ICollection<ResumeEducation> Education { get; set; }
        public ICollection<ResumeCourse> Courses { get; set; }
        public ICollection<ResumeSkill> Skills { get; set; }
        public ICollection<ResumeLanguage> Languages { get; set; }
        public ICollection<ResumeInterest> Interests { get; set; }
    }
}
