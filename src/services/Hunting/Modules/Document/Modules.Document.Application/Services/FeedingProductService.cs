using Mapster;
using Microsoft.Extensions.Logging;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDtos;
using Modules.Document.Application.Interfaces;
using Modules.Document.Domain.Interfaces;
using Shared.Helpers;

namespace Modules.Document.Application.Services
{
    internal class FeedingProductService(
        IFeedingProductRepository feedingProductRepository,
        ILogger<FeedingProductService> logger)
        : IFeedingProductService
    {
        private readonly IFeedingProductRepository _feedingProductRepository
            = feedingProductRepository;
        private readonly ILogger<FeedingProductService> _logger = logger;

        public async Task<FeedingProductResponseDto> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var existingFeedingProduct = await _feedingProductRepository.GetByPredicate(
                e => e.Id == id,
                cancellationToken);

            if (existingFeedingProduct is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingFeedingProduct));
            }

            _feedingProductRepository.Delete(existingFeedingProduct!);

            await _feedingProductRepository.SaveChangesAsync(cancellationToken);

            var response = existingFeedingProduct.Adapt<FeedingProductResponseDto>();

            return response;
        }

        public async Task<List<FeedingProductResponseDto>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var coupons = await _feedingProductRepository.GetAllAsync(cancellationToken);

            var response = coupons.Adapt<List<FeedingProductResponseDto>>();

            return response;
        }

        public async Task<FeedingProductResponseDto> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var coupon = await _feedingProductRepository.GetByPredicate(
                e => e.Id == id,
                cancellationToken);

            if (coupon is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = coupon.Adapt<FeedingProductResponseDto>();

            return response;
        }

        public async Task<FeedingProductResponseDto> UpdateAsync(
            Guid id,
            FeedingProductRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingFeedingProduct = await _feedingProductRepository.GetByPredicate(
                e => e.Id == id,
                cancellationToken);

            if (existingFeedingProduct is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingFeedingProduct));
            }

            var updatedFeedingProduct = request.Adapt(existingFeedingProduct);
            _feedingProductRepository.Delete(updatedFeedingProduct!);

            await _feedingProductRepository.SaveChangesAsync(cancellationToken);

            var response = updatedFeedingProduct.Adapt<FeedingProductResponseDto>();

            return response;
        }
    }
}
