using System;
using System.Collections.Generic;

namespace Sunioj.Core.Resources.Posts
{
    public class PublishedPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public ICollection<TagDTO> Tags { get; set; }
        public string Thumbnail { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public PublishedPostDTO()
        {
            Tags = new List<TagDTO>();
        }

        public override string ToString()
        {
            return $"{Id}: {Title}";
        }
    }
}
