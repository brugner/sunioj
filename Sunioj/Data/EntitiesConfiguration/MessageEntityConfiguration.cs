using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Body).IsRequired();
            builder.Property(x => x.ServicePackageId).IsRequired(false);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder
                .HasOne(x => x.ServicePackage)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
