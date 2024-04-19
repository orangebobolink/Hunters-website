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
    }
}
