using System;

namespace Sunioj.Data.Entities
{
    public class ResumeEducation
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string Title { get; set; }
        public string Institution { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public string Remarks { get; set; }
    }
}
