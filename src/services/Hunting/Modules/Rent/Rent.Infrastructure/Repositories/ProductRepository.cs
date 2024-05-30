using Modules.Document.Infrastructure.Contexts;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces;

namespace Rent.Infrastructure.Repositories
{
    internal class ProductRepository(
        RentDbContext context)
        : BaseRepository<Product>(context),
        IProductRepository
    {
    }
}
