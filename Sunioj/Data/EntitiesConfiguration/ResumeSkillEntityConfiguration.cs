using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class ResumeSkillEntityConfiguration : IEntityTypeConfiguration<ResumeSkill>
    {
        public void Configure(EntityTypeBuilder<ResumeSkill> builder)
        {
            builder.ToTable("ResumeSkills");
            builder.Property(x => x.ResumeId).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }
}
