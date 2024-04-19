using Microsoft.EntityFrameworkCore;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class RaidRepository(DocumentDbContext context)
                : BaseRepository<Raid>(context), IRaidRepository
    {
        public Task<List<Raid>> GetAllIncludeAsync(CancellationToken cancellationToken)
        {
            return _context.Raids
                .Include(r => r.Participants)
                .ToListAsync(cancellationToken);
        }

        public Task<Raid?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Raids
                .Include(r => r.Participants)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }
    }
}
