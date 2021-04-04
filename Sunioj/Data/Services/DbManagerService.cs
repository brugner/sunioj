using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sunioj.Core.Contracts.Services;
using Sunioj.Data.Entities;
using Sunioj.Extensions;
using System;
using System.Linq;

namespace Sunioj.Data.Services
{
    public class DbManagerService : IDbManagerService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<DbManagerService> _logger;

        public DbManagerService(IServiceScopeFactory scopeFactory, ILogger<DbManagerService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;

            Randomizer.Seed = new Random(36535161);
        }

        public void Migrate()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            foreach (var item in context.Database.GetPendingMigrations())
            {
                _logger.LogInformation($"Applying pending migration: {item}");
            }

            context.Database.Migrate();
        }

        public void Seed()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            SeedUsers(context);
            SeedPosts(context);
            SeedServicePackages(context);
            SeedServiceFaqs(context);
            SeedMessages(context);
            SeedSettings(context);
            SeedResume(context);
        }

        private void SeedUsers(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                _logger.LogInformation("Seeding users..");

                var users = GetUserSeed();

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        private void SeedPosts(AppDbContext context)
        {
            if (!context.Posts.Any())
            {
                _logger.LogInformation("Seeding posts..");

                var posts = GetPostsSeeds();

                context.Posts.AddRange(posts);
                context.SaveChanges();
            }
        }

        private void SeedServicePackages(AppDbContext context)
        {
            if (!context.ServicePackages.Any())
            {
                _logger.LogInformation("Seeding service packages..");

                var packages = GetServicePackagesSeeds();

                context.ServicePackages.AddRange(packages);
                context.SaveChanges();
            }
        }

        private void SeedServiceFaqs(AppDbContext context)
        {
            if (!context.ServiceFaqs.Any())
            {
                _logger.LogInformation("Seeding service faqs..");

                var faqs = GetServiceFaqsSeeds();

                context.ServiceFaqs.AddRange(faqs);
                context.SaveChanges();
            }
        }

        private void SeedMessages(AppDbContext context)
        {
            if (!context.Messages.Any())
            {
                _logger.LogInformation("Seeding messages..");

                var messages = GetMessagesSeeds();

                context.Messages.AddRange(messages);
                context.SaveChanges();
            }
        }

        private void SeedSettings(AppDbContext context)
        {
            if (!context.Settings.Any())
            {
                _logger.LogInformation("Seeding settings..");

                var settings = GetSettingsSeeds();

                context.Settings.AddRange(settings);
                context.SaveChanges();
            }
        }

        private void SeedResume(AppDbContext context)
        {
            if (!context.Resumes.Any())
            {
                _logger.LogInformation("Seeding resume..");

                var resume = GetResumeSeed();

                context.Resumes.Add(resume);
                context.SaveChanges();
            }
        }

        private static User GetUserSeed()
        {
            return new User()
            {
                Id = 1,
                Email = "editor@sunioj.com",
                PasswordHash = "1000:RIGtm0DVzuZ4dEtiQncCm4LfujPS9iud:utqaw7AM6GNd4VcbGrplshEbAlE=",
                FirstName = "Editor",
                LastName = "B"
            };
        }

        private static Post[] GetPostsSeeds()
        {
            var tags = new Faker<Tag>()
                .RuleFor(x => x.Name, faker => faker.Lorem.Word().ToLower())
                .RuleFor(x => x.CreatedAt, faker => faker.Date.PastOffset())
                .Generate(10);

            var title = string.Empty;
            int id = 1;

            var posts = new Faker<Post>()
                .RuleFor(x => x.Id, faker => id++)
                .RuleFor(x => x.Title, faker =>
                {
                    title = faker.Commerce.ProductAdjective() + " " + faker.Commerce.ProductName();
                    return title;
                })
                .RuleFor(x => x.Slug, faker => title.ToSlug())
                .RuleFor(x => x.Summary, faker => faker.Lorem.Paragraph())
                .RuleFor(x => x.Content, faker => faker.Lorem.Paragraphs(5))
                .RuleFor(x => x.Tags, faker => faker.Random.ListItems(tags, 3))
                .RuleFor(x => x.IsDraft, faker => faker.Random.Bool(0.25F))
                .RuleFor(x => x.CreatedAt, faker => faker.Date.PastOffset())
                .RuleFor(x => x.UpdatedAt, faker => faker.Date.PastOffset())
                .Generate(25);

            return posts.ToArray();
        }

        private static ServicePackage[] GetServicePackagesSeeds()
        {
            var faker = new Faker();

            var basic = new ServicePackage()
            {
                Id = 1,
                Name = "Basic",
                Description = faker.Lorem.Paragraph(),
                Price = 100,
                CreatedAt = DateTime.Now,
                Order = 1
            };

            var standard = new ServicePackage()
            {
                Id = 2,
                Name = "Standard",
                Description = faker.Lorem.Paragraph(),
                Price = 200,
                CreatedAt = DateTime.Now,
                Order = 2
            };

            var premium = new ServicePackage()
            {
                Id = 3,
                Name = "Premium",
                Description = faker.Lorem.Paragraph(),
                Price = 300,
                CreatedAt = DateTime.Now,
                Order = 3
            };

            return new ServicePackage[] { basic, standard, premium };
        }

        private static ServiceFaq[] GetServiceFaqsSeeds()
        {
            int id = 1;

            var faqs = new Faker<ServiceFaq>()
                .RuleFor(x => x.Id, faker => id)
                .RuleFor(x => x.Question, faker => faker.Lorem.Sentence().Replace(".", "?"))
                .RuleFor(x => x.Answer, faker => faker.Lorem.Paragraph())
                .RuleFor(x => x.Order, faker => id++)
                .RuleFor(x => x.CreatedAt, faker => faker.Date.PastOffset())
                .Generate(10);

            return faqs.ToArray();
        }

        private static Message[] GetMessagesSeeds()
        {
            int id = 1;
            var firstName = string.Empty;
            var lastName = string.Empty;

            var messages = new Faker<Message>()
                .RuleFor(x => x.Id, faker => id++)
                .RuleFor(x => x.Name, faker => faker.Name.FullName())
                .RuleFor(x => x.Name, faker =>
                {
                    firstName = faker.Name.FirstName();
                    lastName = faker.Name.LastName();
                    return $"{firstName} {lastName}";
                })
                .RuleFor(x => x.Email, faker => faker.Internet.Email(firstName, lastName))
                .RuleFor(x => x.Body, faker => faker.Lorem.Paragraph())
                .RuleFor(x => x.ServicePackageId, faker => faker.Random.Int(1, 3))
                .RuleFor(x => x.CreatedAt, faker => faker.Date.PastOffset())
                .Generate(5);

            return messages.ToArray();
        }

        private static Setting[] GetSettingsSeeds()
        {
            return new Setting[]
            {
                new Setting { Name = "site.name", Value = "Sunioj", CreatedAt = DateTime.Now },
                new Setting { Name = "site.image", Value = "", CreatedAt = DateTime.Now },
                new Setting { Name = "editor.name", Value = "Max Doe", CreatedAt = DateTime.Now },
                new Setting { Name = "editor.email", Value = "max.doe@mdmail.com", CreatedAt = DateTime.Now },
                new Setting { Name = "editor.bio", Value = "Hi, my name is Max Doe and I'm a Senior .NET developer. Welcome to my personal website!", CreatedAt = DateTime.Now },
                new Setting { Name = "editor.title", Value = "Senior .NET Developer", CreatedAt = DateTime.Now },
                new Setting { Name = "editor.subtitle", Value = "I'm a software developer specialised in this and that for those web apps. I like writing about this and that on my blog. Want to know how I may help your project? Check out my projects and online resume.", CreatedAt = DateTime.Now },
                new Setting { Name = "blog.title", Value = "My blog", CreatedAt = DateTime.Now },
                new Setting { Name = "blog.subtitle", Value = "Welcome to my blog, here I write about this and that and everything in between.", CreatedAt = DateTime.Now },
                new Setting { Name = "about-me.image", Value = "", CreatedAt = DateTime.Now },
                new Setting { Name = "about-me.what-i-do", Value = "With more than some years of professional experience working as this and that, I can deliver this and that to your help your company. Want to find out more about my experience? Check out my projects and online resume.", CreatedAt = DateTime.Now },
                new Setting { Name = "about-me.expertise.skills", Value = "C#:80;APIs:75;Node:70;Angular:50;React:30", CreatedAt = DateTime.Now },
                new Setting { Name = "about-me.expertise.summary", Value = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer vel consequat turpis. Integer commodo egestas ultricies. Nullam sed pharetra dolor. Phasellus non nibh metus. Nulla eu urna vel ligula fringilla pharetra ac id dui. Sed finibus lorem nisl, et finibus risus elementum vel. Fusce posuere turpis dui, id varius turpis pellentesque ut. Nam vehicula luctus rhoncus. Aliquam sit amet viverra lacus, ac tincidunt turpis. Curabitur eget justo vitae lorem euismod rhoncus. Vivamus lacinia enim eget dui volutpat pretium. Quisque tellus erat, posuere ut quam eu, tincidunt euismod odio. Sed sed euismod massa.", CreatedAt = DateTime.Now },
                new Setting { Name = "socials.linkedin", Value = "https://www.linkedin.com", CreatedAt = DateTime.Now },
                new Setting { Name = "socials.github", Value = "https://github.com", CreatedAt = DateTime.Now },
                new Setting { Name = "socials.stackoverflow", Value = "https://stackoverflow.com", CreatedAt = DateTime.Now },
                new Setting { Name = "socials.twitter", Value = "", CreatedAt = DateTime.Now },
                new Setting { Name = "socials.facebook", Value = "", CreatedAt = DateTime.Now },
                new Setting { Name = "socials.instagram", Value = "", CreatedAt = DateTime.Now },
            };
        }

        private static Resume GetResumeSeed()
        {
            var faker = new Faker();

            var experience = new Faker<ResumeExperience>()
                .RuleFor(x => x.Position, faker => faker.Name.JobTitle())
                .RuleFor(x => x.Company, faker => faker.Company.CompanyName())
                .RuleFor(x => x.From, faker => faker.Date.Past())
                .RuleFor(x => x.To, faker => faker.Date.Past())
                .RuleFor(x => x.Remarks, faker =>
                {
                    var remarks = string.Empty;
                    var max = faker.Random.Int(1, 4);

                    for (int i = 1; i <= max; i++)
                    {
                        remarks += faker.Lorem.Paragraph() + (i < max ? ";" : "");
                    }

                    return remarks;
                })
                .Generate(3);

            var education = new Faker<ResumeEducation>()
                .RuleFor(x => x.Title, faker => faker.Name.JobTitle())
                .RuleFor(x => x.Institution, faker => faker.Company.CompanyName())
                .RuleFor(x => x.From, faker => faker.Date.Past())
                .RuleFor(x => x.To, faker => faker.Date.Past())
                .RuleFor(x => x.Remarks, faker =>
                {
                    var remarks = string.Empty;
                    var max = faker.Random.Int(1, 4);

                    for (int i = 1; i <= max; i++)
                    {
                        remarks += faker.Lorem.Paragraph() + (i < max ? ";" : "");
                    }

                    return remarks;
                })
                .Generate(3);

            var courses = new Faker<ResumeCourse>()
                .RuleFor(x => x.Name, faker => faker.Name.JobTitle())
                .RuleFor(x => x.Institution, faker => faker.Company.CompanyName())
                .RuleFor(x => x.Year, faker => faker.Date.Past().Year)
                .Generate(5);

            var skills = new Faker<ResumeSkill>()
                .RuleFor(x => x.Name, faker => faker.Random.Word())
                .Generate(10);

            var languages = new ResumeLanguage[]
            {
                new ResumeLanguage { Name = "Spanish", Level = "Native" },
                new ResumeLanguage { Name = "English", Level = "Intermediate" }
            };

            var interests = new Faker<ResumeInterest>()
               .RuleFor(x => x.Name, faker => faker.Random.Words(2))
               .Generate(3);

            return new Resume
            {
                Name = faker.Name.FullName(),
                Title = faker.Name.JobTitle(),
                Summary = faker.Lorem.Paragraph(),
                Phone = faker.Phone.PhoneNumber("#### ######"),
                Email = faker.Internet.Email(),
                Website = faker.Internet.Url(),
                Location = faker.Address.City() + ", " + faker.Address.Country(),
                Experience = experience,
                Education = education,
                Courses = courses,
                Skills = skills,
                Languages = languages,
                Interests = interests,
                CreatedAt = DateTime.Now
            };
        }
    }
}
