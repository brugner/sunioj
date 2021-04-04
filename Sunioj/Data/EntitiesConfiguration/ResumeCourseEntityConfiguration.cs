using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class ResumeCourseEntityConfiguration : IEntityTypeConfiguration<ResumeCourse>
    {
        public void Configure(EntityTypeBuilder<ResumeCourse> builder)
        {
            builder.ToTable("ResumeCourses");
            builder.Property(x => x.ResumeId).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Institution).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Year).IsRequired(false);
        }
    }
}
