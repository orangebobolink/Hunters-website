using Modules.Document.Domain.Entities;

namespace Modules.Document.Domain.Interfaces
{
    public interface ITripParticipantRepository
        : IRepository<TripParticipant>, IGetInclude<TripParticipant>
    {
    }
}
