using Mapster;
using Microsoft.Extensions.Logging;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDtos;
using Modules.Document.Application.Interfaces;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Shared.Helpers;

namespace Modules.Document.Application.Services
{
    internal class FeedingService(
        IFeedingRepository feedingRepository,
        ILogger<FeedingService> logger)
        : IFeedingService
    {
        private readonly IFeedingRepository _feedingRepository = feedingRepository;
        private readonly ILogger<FeedingService> _logger = logger;

        public async Task<FeedingResponseDto> CreateAsync(FeedingRequestDto request, CancellationToken cancellationToken)
        {
            //var existingFeeding = await _feedingRepository.GetByIdAsync(id, cancellationToken);

            //if (existingFeedingProduct is null)
            //{
            //    _logger.LogWarning("id is null");
            //    ThrowHelper.ThrowKeyNotFoundException(nameof(existingFeedingProduct));
            //}

            var fedding = request.Adapt<Feeding>();
            fedding.Id = Guid.NewGuid();

            _feedingRepository.Create(fedding!);

            await _feedingRepository.SaveChangesAsync(cancellationToken);

            var response = fedding.Adapt<FeedingResponseDto>();

            return response;
        }

        public async Task<FeedingResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingFeeding = await _feedingRepository.GetByPredicate(f => f.Id == id, cancellationToken);

            if (existingFeeding is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingFeeding));
            }

            _feedingRepository.Delete(existingFeeding!);

            await _feedingRepository.SaveChangesAsync(cancellationToken);

            var response = existingFeeding.Adapt<FeedingResponseDto>();

            return response;
        }

        public async Task<List<FeedingResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var feedings = await _feedingRepository.GetAllAsync(cancellationToken);

            var response = feedings.Adapt<List<FeedingResponseDto>>();

            return response;
        }

        public async Task<List<FeedingResponseDto>> GetAllIncludeAsync(CancellationToken cancellationToken)
        {
            var feedings = await _feedingRepository.GetAllIncludeAsync(cancellationToken);

            var response = feedings.Adapt<List<FeedingResponseDto>>();

            return response;
        }

        public async Task<FeedingResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var feeding = await _feedingRepository.GetByPredicate(e => e.Id == id, cancellationToken);

            if (feeding is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = feeding.Adapt<FeedingResponseDto>();

            return response;
        }

        public async Task<FeedingResponseDto?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken)
        {
            var feeding = await _feedingRepository.GetByIdIncludeAsync(id, cancellationToken);

            if (feeding is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = feeding.Adapt<FeedingResponseDto>();

            return response;
        }

        public async Task<FeedingResponseDto> UpdateAsync(Guid id, FeedingRequestDto request, CancellationToken cancellationToken)
        {
            var existingFeeding = await _feedingRepository.GetByPredicate(f => f.Id == id, cancellationToken);

            if (existingFeeding is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingFeeding));
            }

            var feeding = request.Adapt(existingFeeding);

            _feedingRepository.Update(feeding!);

            await _feedingRepository.SaveChangesAsync(cancellationToken);

            var response = existingFeeding.Adapt<FeedingResponseDto>();

            return response;
        }
    }
}
