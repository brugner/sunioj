using System;

namespace Sunioj.Core.Resources.Resumes
{
    public class ResumeEducationDTO
    {
        public string Title { get; set; }
        public string Institution { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public string Remarks { get; set; }
    }
}
