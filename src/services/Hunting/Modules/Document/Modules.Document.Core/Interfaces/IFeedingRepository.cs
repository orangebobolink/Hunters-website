using Modules.Document.Domain.Entities;

namespace Modules.Document.Domain.Interfaces
{
    public interface IFeedingRepository
        : IRepository<Feeding>, IGetInclude<Feeding>
    {
    }
}
