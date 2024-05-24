using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.DataSeed
{
    internal static class DataSeederGenerator
    {
        public static List<IdentityRole<Guid>> GetRoles()
        {
            List<string> existingRoles = [
               Role.Admin,
                Role.User,
                Role.Manager,
                Role.Director,
                Role.Ranger
           ];

            var roles = existingRoles.Select(roleName =>
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = roleName,
                    NormalizedName = roleName.ToUpperInvariant()
                })
                .ToList();

            return roles;
        }

        public static List<User> GetUsers()
        {
            var passwordHasher = new PasswordHasher<User>();

            var users = new List<User>()
            {
                new User {
                    Id = Guid.NewGuid(),
                    UserName = "user",
                    NormalizedUserName = "USER",
                    Email = "user@gmail.com",
                    NormalizedEmail = "USER@GMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "password"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LastName = "lastUser",
                    FirstName = "firstUser",
                    MiddleName = "middleUser"
                },
                new User {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "password"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LastName = "lastAdmin",
                    FirstName = "firstAdmin",
                    MiddleName = "middleUser"
                },
                new User {
                    Id = Guid.NewGuid(),
                    UserName = "manager",
                    NormalizedUserName = "MANAGER",
                    Email = "manager@gmail.com",
                    NormalizedEmail = "MANAGER@GMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "password"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LastName = "lastManager",
                    FirstName = "firstManager",
                    MiddleName = "middleManager"
                },
                new User {
                    Id = Guid.NewGuid(),
                    UserName = "ranger",
                    NormalizedUserName = "RANGER",
                    Email = "ranger@gmail.com",
                    NormalizedEmail = "RANGER@GMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(null, "password"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LastName = "lastRanger",
                    FirstName = "firstRanger",
                    MiddleName = "middleRanger"
                },
            };

            return users;
        }

        public static List<IdentityUserRole<Guid>> GetUserRole(List<User> users, List<IdentityRole<Guid>> roles)
        {
            var userRoles = new List<IdentityUserRole<Guid>>()
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
                },
                new IdentityUserRole<Guid>
                {
                    UserId = users[2].Id,
                    RoleId = roles.First(q => q.Name == Role.Manager).Id
                },
                new IdentityUserRole<Guid>
                {
                    UserId = users[3].Id,
                    RoleId = roles.First(q => q.Name == Role.Ranger).Id
                }
            };

            return userRoles;
        }
    }
}