using System;

namespace Sunioj.Data.Entities
{
    public class ResumeExperience
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public string Remarks { get; set; }
    }
}
