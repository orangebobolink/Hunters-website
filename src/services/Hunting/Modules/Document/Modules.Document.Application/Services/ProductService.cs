using Modules.Document.Application.Interfaces;
using Modules.Document.Domain.Enums;

namespace Modules.Document.Application.Services
{
    internal class ProductService : IProductService
    {
        public Task<List<string>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return Task.FromResult(
                Enum.GetValues<Product>()
                    .Select(p => p.ToString())
                    .ToList()
                );
        }
    }
}
