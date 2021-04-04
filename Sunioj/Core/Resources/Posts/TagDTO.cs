namespace Sunioj.Core.Resources.Posts
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TagDTO()
        {

        }

        public TagDTO(string name)
        {
            Name = name;
        }
    }
}
