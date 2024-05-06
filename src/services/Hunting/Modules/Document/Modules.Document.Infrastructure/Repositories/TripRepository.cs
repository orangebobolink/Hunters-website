using Microsoft.EntityFrameworkCore;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class TripRepository(
        DocumentDbContext context)
        : BaseRepository<Trip>(context),
        ITripRepository
    {
        public Task<List<Trip>> GetAllIncludeAsync(CancellationToken cancellationToken)
        {
            return _context.Trips
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Issued)
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Received)
                .Include(t => t.Buyer)
                .Include(t => t.TripParticipants)
                    .ThenInclude(tp => tp.Participant)
                .ToListAsync(cancellationToken);
        }

        public Task<Trip?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Trips
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Issued)
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Received)
                .Include(t => t.Buyer)
                .Include(t => t.TripParticipants)
                    .ThenInclude(tp => tp.Participant)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

    }
}
