using Chat.Domain.Entities;
using Chat.Infrastructure.Contexts;
using Chat.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories
{
    internal class MessageRepository(ApplicationDbContext context)
        : BaseRepository<Message>(context), IMessageRepository
    {
        public override async Task<List<Message>> GetAllAsync()
        {
            return await _context.Messages
                .AsNoTracking()
                .Where(m => !m.IsDeleted)
                .ToListAsync();
        }

        public override async Task<Message?> GetByIdAsync(Guid id)
        {
            return await _context.Messages
                .AsNoTracking()
                .Where(m => !m.IsDeleted)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<List<Message>> GetMessagesByUserId(Guid userId)
        {
            return _context.Messages
                .AsNoTracking()
                .Where(m => !m.IsDeleted && m.UserId == userId)
                .ToListAsync();
        }

        public void SoftDelete(Message entity)
        {
            entity.IsDeleted = true;
            _context.Update(entity);
        }
    }
}
