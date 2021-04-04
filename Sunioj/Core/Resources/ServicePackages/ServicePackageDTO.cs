using System;

namespace Sunioj.Core.Resources.ServicePackages
{
    public class ServicePackageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Order { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
