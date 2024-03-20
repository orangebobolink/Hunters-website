using Chat.Domain.Entities;
using Chat.Infrastructure.Contexts;
using Chat.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories
{
    internal class UserRepository(ApplicationDbContext context)
        : BaseRepository<User>(context), IUserRepository
    {
        public override async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
               .AsNoTracking()
               .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
