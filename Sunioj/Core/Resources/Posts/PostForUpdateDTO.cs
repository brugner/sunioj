using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sunioj.Core.Resources.Posts
{
    public class PostForUpdateDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public ICollection<TagDTO> Tags { get; set; }

        [Required]
        public bool IsDraft { get; set; }

        public IFormFile Thumbnail { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
