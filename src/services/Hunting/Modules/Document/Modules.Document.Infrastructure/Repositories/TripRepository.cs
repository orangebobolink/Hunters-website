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
                .Include(t => t.Issued)
                .Include(t => t.Received)
                .Include(t => t.Accepted)
                .Include(t => t.TripParticipants)
                    .ThenInclude(tp => tp.Participant)
                .ToListAsync(cancellationToken);
        }

        public Task<Trip?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Trips
                .Include(t => t.Permission)
                .Include(t => t.Issued)
                .Include(t => t.Received)
                .Include(t => t.Accepted)
                .Include(t => t.TripParticipants)
                    .ThenInclude(tp => tp.Participant)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

    }
}
