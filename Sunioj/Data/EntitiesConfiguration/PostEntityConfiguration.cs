using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;
using System.Collections.Generic;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Summary).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Slug).IsUnique();
            builder.Property(x => x.CreatedAt).IsRequired();

            builder
                .HasMany(x => x.Tags)
                .WithMany(x => x.Posts)
                .UsingEntity<Dictionary<string, object>>("PostTags",
                    j => j
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_PostTags_Tags_TagId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostTags_Posts_PostId")
                        .OnDelete(DeleteBehavior.ClientCascade));
        }
    }
}
