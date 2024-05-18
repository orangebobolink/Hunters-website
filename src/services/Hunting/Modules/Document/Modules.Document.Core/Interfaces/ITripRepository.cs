using Modules.Document.Domain.Entities;

namespace Modules.Document.Domain.Interfaces
{
    public interface ITripRepository
        : IRepository<Trip>, IGetInclude<Trip>
    {
        public Task<List<Trip>> GetByParticipantIdIncludeAsync(Guid id, CancellationToken cancellationToken);
    }
}
