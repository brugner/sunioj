using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class ServicePackageEntityConfiguration : IEntityTypeConfiguration<ServicePackage>
    {
        public void Configure(EntityTypeBuilder<ServicePackage> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Order).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
