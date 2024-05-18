using Modules.Document.Infrastructure.Contexts;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces;

namespace Rent.Infrastructure.Repositories
{
    internal class RentProductRepository(
        RentDbContext context)
                : BaseRepository<RentProduct>(context), IRentProductRepository
    {
    }
}
