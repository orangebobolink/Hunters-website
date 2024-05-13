using Mapster;
using Microsoft.Extensions.Logging;
using Rent.Application.Dtos.RequestDtos;
using Rent.Application.Dtos.ResponseDtos;
using Rent.Application.Interfaces;
using Rent.Domain.Entities;
using Rent.Domain.Enums;
using Rent.Domain.Interfaces;
using Shared.Helpers;

namespace Rent.Application.Services
{
    internal class RentProductService(
        IProductRepository productRepository,
        IRentProductRepository rentProductRepository,
        ILogger<RentProductService> logger)
        : IRentProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IRentProductRepository _rentProductRepository = rentProductRepository;
        private readonly ILogger<RentProductService> _logger = logger;

        public async Task<RentProductResponseDto> CreateAsync(
            RentProductRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByPredicate(
               t => t.Id == request.ProductId,
               cancellationToken,
               true);

            if (existingProduct is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingProduct));
            }

            var rentProduct = request.Adapt<RentProduct>();
            rentProduct.Id = Guid.NewGuid();
            rentProduct.Status = RentStatus.Pending;
            existingProduct!.RentProducts
                .Add(rentProduct);

            _productRepository.Update(existingProduct);

            await _productRepository.SaveChangesAsync(cancellationToken);

            var response = rentProduct.Adapt<RentProductResponseDto>();

            return response;
        }

        public async Task<RentProductResponseDto> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var existingProduct = await _rentProductRepository.GetByPredicate(
                p => p.Id == id,
                cancellationToken);

            if (existingProduct is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingProduct));
            }

            _rentProductRepository.Delete(existingProduct!);

            await _productRepository.SaveChangesAsync(cancellationToken);

            var response = existingProduct.Adapt<RentProductResponseDto>();

            return response;
        }

        public async Task<IEnumerable<RentProductResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var rentsProducts = await _rentProductRepository.GetAllAsync(cancellationToken, true);

            var response = rentsProducts.Adapt<IEnumerable<RentProductResponseDto>>();

            return response;
        }

        public async Task<RentProductResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var rentsProduct = await _rentProductRepository.GetByPredicate(
                p => p.Id == id,
                cancellationToken,
                true);

            if (rentsProduct is null)
            {
                ThrowHelper.ThrowKeyNotFoundException(id.ToString());
            }

            var response = rentsProduct.Adapt<RentProductResponseDto>();

            return response;
        }

        public async Task<RentProductResponseDto> UpdateAsync(
            Guid id,
            RentProductRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingProduct = await _rentProductRepository.GetByPredicate(
                 t => t.Id == id,
                 cancellationToken);

            if (existingProduct is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingProduct));
            }

            var product = request.Adapt(existingProduct);

            _rentProductRepository.Update(product!);

            await _rentProductRepository.SaveChangesAsync(cancellationToken);

            var response = product.Adapt<RentProductResponseDto>();

            return response;
        }
    }
}
