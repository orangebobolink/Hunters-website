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
    internal class LandService(
        ILandRepository landRepository,
        ILogger<LandService> logger)
        : ILandService
    {
        private readonly ILandRepository _landRepository = landRepository;
        private readonly ILogger<LandService> _logger = logger;

        public async Task<LandResponseDto> CreateAsync(
            LandRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingLand = await _landRepository.GetByPredicate(
                l => l.Name == request.Name,
                cancellationToken);

            if (existingLand is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingLand));
            }

            var land = request.Adapt<Land>();
            land.Id = Guid.NewGuid();

            _landRepository.Create(land!);

            await _landRepository.SaveChangesAsync(cancellationToken);

            var response = land.Adapt<LandResponseDto>();

            return response;
        }

        public async Task<LandResponseDto> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var existingLand = await _landRepository.GetByPredicate(
                e => e.Id == id, cancellationToken);

            if (existingLand is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingLand));
            }

            _landRepository.Delete(existingLand!);

            await _landRepository.SaveChangesAsync(cancellationToken);

            var response = existingLand.Adapt<LandResponseDto>();

            return response;
        }

        public async Task<List<LandResponseDto>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var feedings = await _landRepository.GetAllAsync(cancellationToken);

            var response = feedings.Adapt<List<LandResponseDto>>();

            return response;
        }

        public async Task<LandResponseDto> GetByIdAsync(
            Guid id, CancellationToken cancellationToken)
        {
            var feeding = await _landRepository.GetByPredicate(
                e => e.Id == id, cancellationToken);

            if (feeding is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = feeding.Adapt<LandResponseDto>();

            return response;
        }

        public async Task<LandResponseDto> UpdateAsync(
            Guid id,
            LandRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingLand = await _landRepository.GetByPredicate(
                e => e.Id == id, cancellationToken);

            if (existingLand is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingLand));
            }

            var feeding = request.Adapt(existingLand);

            _landRepository.Update(feeding!);

            await _landRepository.SaveChangesAsync(cancellationToken);

            var response = feeding.Adapt<LandResponseDto>();

            return response;
        }
    }
}
