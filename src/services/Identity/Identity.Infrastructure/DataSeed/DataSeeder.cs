using Identity.Domain.Entities;
using Identity.Infrastructure.Contexts;
using Identity.Infrastructure.Interfaces;
using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Messages.DataSeedMessages;
using Shared.Messages.UserMessages;

namespace Identity.Infrastructure.DataSeed
{
    internal class DataSeeder
        : IDataSeeder
    {
        private readonly IBus _bus;
        protected readonly ApplicationDbContext _context;
        private readonly List<IdentityRole<Guid>> _roles;
        private readonly List<User> _users;
        private readonly List<IdentityUserRole<Guid>> _userRole;

        public DataSeeder(IBus bus, ApplicationDbContext context)
        {
            _bus = bus;
            _context = context;
            _roles = GetRoles();
            _users = GetUsers();
            _userRole = GetUserRole();
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedUserRole();
        }

        public async Task SeedMessageAsync()
        {
            var userNames = _users.Select(u => u.UserName).ToList();
            var users = await _context.Users.Where(u => userNames.Contains(u.UserName)).ToListAsync();
            var usersMessage = users.Adapt<List<CreateUserMessage>>();
            var message = new UserDataSeedMessage() { Users = usersMessage };

            await _bus.Publish(message);
        }

        private List<IdentityRole<Guid>> GetRoles()
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

        private List<User> GetUsers()
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
                    FirstName = "firstUser"
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
                    FirstName = "firstAdmin"
                },
            };

            return users;
        }

        private List<IdentityUserRole<Guid>> GetUserRole()
        {
            var userRoles = new List<IdentityUserRole<Guid>>()
            {
                new IdentityUserRole<Guid>
                {
                    UserId = _users[0].Id,
                    RoleId = _roles.First(q => q.Name == Role.User).Id
                },
                new IdentityUserRole<Guid>
                {
                    UserId = _users[1].Id,
                    RoleId = _roles.First(q => q.Name == Role.Admin).Id
                }
            };

            return userRoles;
        }

        private async Task SeedRolesAsync()
        {
            if (!await _context.Roles.AnyAsync())
            {
                _context.AddRange(_roles);

                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedUsersAsync()
        {
            if (!await _context.Users.AnyAsync())
            {
                _context.AddRange(_roles);

                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedUserRole()
        {
            if (!await _context.UserRoles.AnyAsync())
            {
                _context.UserRoles.AddRange(_userRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
