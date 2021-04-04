using System;
using System.ComponentModel.DataAnnotations;

namespace Sunioj.Core.Resources.Resumes
{
    public class ResumeExperienceForUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Position { get; set; }

        [Required]
        [MaxLength(50)]
        public string Company { get; set; }

        [Required]
        public DateTime From { get; set; }

        public DateTime? To { get; set; }
        public string Remarks { get; set; }
    }
}
