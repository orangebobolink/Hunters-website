using Microsoft.EntityFrameworkCore;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;
using System.Linq;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class UserRepository(
        DocumentDbContext context)
        : BaseRepository<User>(context),
        IUserRepository
    {
        public async Task<List<User>> GetAllExistsUsers(List<User> users)
        {
            var userIds = users.Select(u => u.Id)
                            .ToList();
            return await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync();
        }
    }
}
