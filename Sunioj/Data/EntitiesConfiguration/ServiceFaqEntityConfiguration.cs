using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class ServiceFaqEntityConfiguration : IEntityTypeConfiguration<ServiceFaq>
    {
        public void Configure(EntityTypeBuilder<ServiceFaq> builder)
        {
            builder.Property(x => x.Question).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Answer).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Order).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
