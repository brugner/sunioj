namespace Sunioj.Data.Entities
{
    public class ServiceFaq : Entity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Order { get; set; }
    }
}
