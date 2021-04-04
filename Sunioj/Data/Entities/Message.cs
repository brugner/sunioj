namespace Sunioj.Data.Entities
{
    public class Message : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public int? ServicePackageId { get; set; }
        public ServicePackage ServicePackage { get; set; }
    }
}
