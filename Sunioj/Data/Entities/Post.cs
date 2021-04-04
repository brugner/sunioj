using System.Collections.Generic;

namespace Sunioj.Data.Entities
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public bool IsDraft { get; set; }
        public string Thumbnail { get; set; }

        public Post()
        {
            Tags = new List<Tag>();
        }

        public override string ToString()
        {
            return $"{Id}: {Title}";
        }
    }
}
