namespace Sunioj.Data.Entities
{
    public class ServicePackage : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Order { get; set; }
    }
}
