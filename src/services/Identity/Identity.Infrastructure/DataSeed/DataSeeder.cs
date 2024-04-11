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
            _roles = DataSeederGenerator.GetRoles();
            _users = DataSeederGenerator.GetUsers();
            _userRole = DataSeederGenerator.GetUserRole(_users, _roles);
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
