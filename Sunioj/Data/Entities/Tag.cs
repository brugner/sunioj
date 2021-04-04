using System.Collections.Generic;

namespace Sunioj.Data.Entities
{
    public class Tag : Entity
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }

        public Tag()
        {
            Posts = new List<Post>();
        }

        public Tag(string name) : this()
        {
            Name = name;
        }
    }
}
