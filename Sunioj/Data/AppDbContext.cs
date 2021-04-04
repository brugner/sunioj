using Microsoft.EntityFrameworkCore;
using Sunioj.Data.Entities;
using Sunioj.Data.EntitiesConfiguration;

namespace Sunioj.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ServicePackage> ServicePackages { get; set; }
        public DbSet<ServiceFaq> ServiceFaqs { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Resume> Resumes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TagEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ServicePackageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceFaqEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SettingEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ResumeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ResumeExperienceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ResumeCourseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ResumeSkillEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ResumeLanguageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ResumeInterestEntityConfiguration());
        }
    }
}
