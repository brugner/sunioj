namespace Sunioj.Data.Entities
{
    public class ResumeCourse
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string Name { get; set; }
        public string Institution { get; set; }
        public int? Year { get; set; }
    }
}
