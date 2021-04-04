using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class ResumeEducationEntityConfiguration : IEntityTypeConfiguration<ResumeEducation>
    {
        public void Configure(EntityTypeBuilder<ResumeEducation> builder)
        {
            builder.ToTable("ResumeEducations");
            builder.Property(x => x.ResumeId).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Institution).HasMaxLength(50).IsRequired();
            builder.Property(x => x.From).IsRequired();
            builder.Property(x => x.To).IsRequired(false);
            builder.Property(x => x.Remarks);
        }
    }
}
