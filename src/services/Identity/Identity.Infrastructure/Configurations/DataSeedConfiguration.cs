using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Configurations
{
    internal static class DataSeedConfiguration
    {
        public static void ApplyDataSeed(this ModelBuilder builder)
        {
            List<IdentityRole<Guid>> roles = builder.SeedRoles();

            List<User> users = builder.SeedUsers();

            builder.SeedUserRole(users, roles);
        }

        private static List<IdentityRole<Guid>> SeedRoles(this ModelBuilder builder)
        {
            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>()
            {
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = Role.Admin,
                    NormalizedName = Role.Admin.ToUpperInvariant()
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = Role.User,
                    NormalizedName = Role.User.ToUpperInvariant()
                }
            };

            builder.Entity<IdentityRole<Guid>>()
                .HasData(roles);

            return roles;
        }

        private static List<User> SeedUsers(this ModelBuilder builder)
        {
            var passwordHasher = new PasswordHasher<User>();

            List<User> users = new List<User>()
            {
                new User {
                    Id = Guid.NewGuid(),
                    UserName = "user",
                    NormalizedUserName = "USER",
                    Email = "user@gmail.com",
                    NormalizedEmail = "USER@GMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "password"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new User {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "password"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
            };


            builder.Entity<User>()
                .HasData(users);

            return users;
        }

        private static void SeedUserRole(this ModelBuilder builder, List<User> users, List<IdentityRole<Guid>> roles)
        {
            List<IdentityUserRole<Guid>> userRoles = new List<IdentityUserRole<Guid>>()
            {
                new IdentityUserRole<Guid>
                {
                    UserId = users[0].Id,
                    RoleId = roles.First(q => q.Name == Role.User).Id
                },
                new IdentityUserRole<Guid>
                {
                    UserId = users[1].Id,
                    RoleId = roles.First(q => q.Name == Role.Admin).Id
                }
            };

            builder.Entity<IdentityUserRole<Guid>>()
                .HasData(userRoles);

            builder.Entity<IdentityUserRole<Guid>>(userRole =>
            {
                userRole.HasKey(pr => new
                {
                    pr.UserId,
                    pr.RoleId,
                });
            });
        }
    }
}