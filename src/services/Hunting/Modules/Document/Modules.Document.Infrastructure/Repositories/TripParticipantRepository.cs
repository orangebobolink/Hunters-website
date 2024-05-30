using Microsoft.EntityFrameworkCore;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class TripParticipantRepository(
        DocumentDbContext context)
        : BaseRepository<TripParticipant>(context),
        ITripParticipantRepository
    {
        public Task<List<TripParticipant>> GetAllIncludeAsync(CancellationToken cancellationToken)
        {
            return _context.TripParticipants
                .Include(tp => tp.Participant)
                .Include(tp => tp.HuntingLicense)
                .Include(tp => tp.Trip)
                .ToListAsync(cancellationToken);
        }

        public Task<TripParticipant?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.TripParticipants
                .Include(tp => tp.Participant)
                .Include(tp => tp.HuntingLicense)
                .Include(tp => tp.Trip)
                .FirstOrDefaultAsync(tp => tp.Id == id, cancellationToken);
        }
    }
}
