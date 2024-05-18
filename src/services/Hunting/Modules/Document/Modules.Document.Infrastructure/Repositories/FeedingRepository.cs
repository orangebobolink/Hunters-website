using Microsoft.EntityFrameworkCore;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class FeedingRepository(DocumentDbContext context)
                : BaseRepository<Feeding>(context), IFeedingRepository
    {
        public Task<List<Feeding>> GetAllIncludeAsync(CancellationToken cancellationToken)
        {
            return _context.Feedings
                .AsSingleQuery()
                .Include(f => f.Issued)
                .Include(f => f.Issued)
                .Include(f => f.Products)
                .Include(f => f.Land)
                .ToListAsync(cancellationToken);
        }

        public Task<Feeding?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Feedings
                .AsSingleQuery()
                .Include(f => f.Issued)
                .Include(f => f.Received)
                .Include(f => f.Products)
                .Include(f => f.Land)
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }
    }
}
