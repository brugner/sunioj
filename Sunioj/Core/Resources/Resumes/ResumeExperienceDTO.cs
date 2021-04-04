using System;

namespace Sunioj.Core.Resources.Resumes
{
    public class ResumeExperienceDTO
    {
        public string Position { get; set; }
        public string Company { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public string Remarks { get; set; }
    }
}
