using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class ResumeLanguageEntityConfiguration : IEntityTypeConfiguration<ResumeLanguage>
    {
        public void Configure(EntityTypeBuilder<ResumeLanguage> builder)
        {
            builder.ToTable("ResumeLanguages");
            builder.Property(x => x.ResumeId).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Level).HasMaxLength(50).IsRequired();
        }
    }
}
