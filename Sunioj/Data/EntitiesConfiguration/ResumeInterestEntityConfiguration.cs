using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class ResumeInterestEntityConfiguration : IEntityTypeConfiguration<ResumeInterest>
    {
        public void Configure(EntityTypeBuilder<ResumeInterest> builder)
        {
            builder.ToTable("ResumeInterests");
            builder.Property(x => x.ResumeId).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }
}
