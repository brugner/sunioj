﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sunioj.Data.Entities;

namespace Sunioj.Data.EntitiesConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
        }
    }
}
