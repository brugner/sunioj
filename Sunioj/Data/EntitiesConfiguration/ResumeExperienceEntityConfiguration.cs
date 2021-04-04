using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class ResumeExperienceEntityConfiguration : IEntityTypeConfiguration<ResumeExperience>
    {
        public void Configure(EntityTypeBuilder<ResumeExperience> builder)
        {
            builder.ToTable("ResumeExperiences");
            builder.Property(x => x.ResumeId).IsRequired();
            builder.Property(x => x.Position).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Company).HasMaxLength(50).IsRequired();
            builder.Property(x => x.From).IsRequired();
            builder.Property(x => x.To).IsRequired(false);
            builder.Property(x => x.Remarks);
        }
    }
}
