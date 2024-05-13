using Mapster;
using Microsoft.Extensions.Logging;
using Rent.Application.Dtos.RequestDtos;
using Rent.Application.Dtos.ResponseDtos;
using Rent.Application.Interfaces;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces;
using Shared.Helpers;

namespace Rent.Application.Services
{
    internal class ProductService(
        IProductRepository productRepository,
        ILogger<ProductService> logger)
        : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ILogger<ProductService> _logger = logger;

        public async Task<ProductResponseDto> CreateAsync(
            ProductRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByPredicate(
                t => t.Name == request.Name,
                cancellationToken);

            if (existingProduct is not null)
            {
                _logger.LogWarning("id is not null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingProduct));
            }

            var product = request.Adapt<Product>();
            product.Id = Guid.NewGuid();
            product.CreatedAt = DateTime.Now;

            _productRepository.Create(product!);

            await _productRepository.SaveChangesAsync(cancellationToken);

            var response = product.Adapt<ProductResponseDto>();

            return response;
        }

        public async Task<ProductResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByPredicate(
                t => t.Id == id,
                cancellationToken);

            if (existingProduct is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingProduct));
            }

            _productRepository.Delete(existingProduct!);

            await _productRepository.SaveChangesAsync(cancellationToken);

            var response = existingProduct.Adapt<ProductResponseDto>();

            return response;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var product = await _productRepository
                 .GetAllAsync(cancellationToken);

            var response = product.Adapt<IEnumerable<ProductResponseDto>>();

            return response;
        }

        public async Task<ProductResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productRepository
                .GetByPredicate(
                p => p.Id == id,
                cancellationToken,
                true);

            if (product is null)
            {
                ThrowHelper.ThrowKeyNotFoundException(id.ToString());
            }

            var response = product.Adapt<ProductResponseDto>();

            return response;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetByType(string type, CancellationToken cancellationToken)
        {
            var product = await _productRepository
                .GetAllByPredicate(
                p => nameof(p.Type) == type,
                cancellationToken);

            var response = product.Adapt<IEnumerable<ProductResponseDto>>();

            return response;
        }

        public async Task<ProductResponseDto> UpdateAsync(
            Guid id,
            ProductRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByPredicate(
                t => t.Id == id,
                cancellationToken);

            if (existingProduct is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingProduct));
            }

            var product = request.Adapt(existingProduct);
            product!.UpdatedAt = DateTime.UtcNow;

            _productRepository.Update(product!);

            await _productRepository.SaveChangesAsync(cancellationToken);

            var response = product.Adapt<ProductResponseDto>();

            return response;
        }
    }
}
