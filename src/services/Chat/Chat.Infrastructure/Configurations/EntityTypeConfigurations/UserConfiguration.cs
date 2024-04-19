﻿using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Configurations.EntityTypeConfigurations
{
    internal class UserConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.AvatarUrl)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(u => u.Groups)
                    .WithMany(g => g.Users)
                    .UsingEntity<UserGroup>(
                        j => j
                            .HasOne<Group>()
                            .WithMany()
                            .HasForeignKey("GroupId"),
                        j => j
                            .HasOne<User>()
                            .WithMany()
                            .HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "GroupId");
                            j.ToTable("UserGroups");
                        });
        }
    }
}
