using System;
using System.ComponentModel.DataAnnotations;

namespace Sunioj.Core.Resources.Resumes
{
    public class ResumeEducationForUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Institution { get; set; }

        [Required]
        public DateTime From { get; set; }

        public DateTime? To { get; set; }
        public string Remarks { get; set; }
    }
}
