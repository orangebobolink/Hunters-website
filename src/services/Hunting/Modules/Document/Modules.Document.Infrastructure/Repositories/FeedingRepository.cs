using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class FeedingRepository(DocumentDbContext context)
                : BaseRepository<Feeding>(context), IFeedingRepository
    {
    }
}
