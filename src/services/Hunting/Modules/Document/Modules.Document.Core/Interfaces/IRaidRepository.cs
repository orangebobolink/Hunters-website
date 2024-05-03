using Modules.Document.Domain.Entities;

namespace Modules.Document.Domain.Interfaces
{
    public interface IRaidRepository
        : IRepository<Raid>, IGetInclude<Raid>
    {
        Task<List<Raid>> GetRaidsByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
