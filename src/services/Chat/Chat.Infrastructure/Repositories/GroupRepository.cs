using Chat.Domain.Entities;
using Chat.Infrastructure.Contexts;
using Chat.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories
{
    internal class GroupRepository(ApplicationDbContext context)
        : BaseRepository<Group>(context), IGroupRepository
    {
        public override async Task<List<Group>> GetAllAsync()
        {
            return await _context.Groups
                .AsNoTracking()
                .Where(g => !g.IsDeleted)
                .ToListAsync();
        }

        public override async Task<Group?> GetByIdAsync(Guid id)
        {
            return await _context.Groups
                .AsNoTracking()
                .Where(g => !g.IsDeleted)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<Group>> GetGroupsByUserIdAsync(Guid userId)
        {
            return await _context.Groups
                .Include(g => g.Messages)
                .Include(g => g.Users)
                .ThenInclude(g => g.User)
                .AsNoTracking()
                .Where(g => !g.IsDeleted && g.Users.Any(m => m.UserId == userId))
                .ToListAsync();
        }

        public void SoftDelete(Group entity)
        {
            entity.IsDeleted = true;
            _context.Update(entity);
        }
    }
}
