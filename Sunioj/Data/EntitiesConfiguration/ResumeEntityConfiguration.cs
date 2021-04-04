using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class ResumeEntityConfiguration : IEntityTypeConfiguration<Resume>
    {
        public void Configure(EntityTypeBuilder<Resume> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Summary).HasMaxLength(600).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.Phone).HasMaxLength(15);
            builder.Property(x => x.Website).HasMaxLength(100);
            builder.Property(x => x.Location).HasMaxLength(100);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder
                .HasMany(x => x.Experience)
                .WithOne();

            builder
                .HasMany(x => x.Education)
                .WithOne();

            builder
                .HasMany(x => x.Courses)
                .WithOne();

            builder
                .HasMany(x => x.Skills)
                .WithOne();

            builder
                .HasMany(x => x.Languages)
                .WithOne();

            builder
                .HasMany(x => x.Interests)
                .WithOne();
        }
    }
}
