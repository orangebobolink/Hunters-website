using Identity.Domain.Entities;
using Identity.Infrastructure.Contexts;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
    internal class DataSeeder(
        IBus bus, ApplicationDbContext context) 
        : IDataSeeder
    {
        private readonly IBus _bus = bus;
        protected readonly ApplicationDbContext _context = context;

        public async Task SeedAsync(ModelBuilder builder)
        {
            List<IdentityRole<Guid>> roles = SeedRoles(builder);

            await _bus.Publish(new User());

            List<User> users = SeedUsers(builder);

            SeedUserRole(builder, users, roles);
        }

        private List<IdentityRole<Guid>> SeedRoles(ModelBuilder builder)
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

            builder.Entity<IdentityRole<Guid>>()
                .HasData(roles);

            return roles;
        }

        private static List<User> SeedUsers(ModelBuilder builder)
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

        private static void SeedUserRole(ModelBuilder builder, List<User> users, List<IdentityRole<Guid>> roles)
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
