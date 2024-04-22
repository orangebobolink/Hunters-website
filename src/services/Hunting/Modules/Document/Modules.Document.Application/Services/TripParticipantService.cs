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
    internal class TripParticipantService(
        ITripParticipantRepository tripParticipantRepository,
        ILogger<TripParticipantService> logger)
        : ITripParticipantService
    {
        private readonly ITripParticipantRepository _tripParticipantRepository = tripParticipantRepository;
        private readonly ILogger<TripParticipantService> _logger = logger;

        public async Task<TripParticipantResponseDto> CreateAsync(TripParticipantRequestDto request, CancellationToken cancellationToken)
        {
            var existingTripParticipant = await _tripParticipantRepository.GetByPredicate(
                t => t.TripId == request.TripId
                    && t.HuntingLicenseId == request.HuntingLicenseId,
                cancellationToken);

            if (existingTripParticipant is not null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingTripParticipant));
            }

            var tripParticipant = request.Adapt<TripParticipant>();
            tripParticipant.Id = Guid.NewGuid();

            _tripParticipantRepository.Create(tripParticipant!);

            await _tripParticipantRepository.SaveChangesAsync(cancellationToken);

            var response = tripParticipant.Adapt<TripParticipantResponseDto>();

            return response;
        }

        public async Task<TripParticipantResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingTripParticipant = await _tripParticipantRepository.GetByPredicate(
                e => e.Id == id,
                cancellationToken);

            if (existingTripParticipant is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingTripParticipant));
            }

            _tripParticipantRepository.Delete(existingTripParticipant!);

            await _tripParticipantRepository.SaveChangesAsync(cancellationToken);

            var response = existingTripParticipant.Adapt<TripParticipantResponseDto>();

            return response;
        }

        public async Task<TripParticipantResponseDto> UpdateAsync(
            Guid id,
            TripParticipantRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingTripParticipant = await _tripParticipantRepository.GetByPredicate(
                e => e.Id == id,
                cancellationToken);

            if (existingTripParticipant is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingTripParticipant));
            }

            var permission = request.Adapt(existingTripParticipant);

            _tripParticipantRepository.Update(permission!);

            await _tripParticipantRepository.SaveChangesAsync(cancellationToken);

            var response = existingTripParticipant.Adapt<TripParticipantResponseDto>();

            return response;
        }
    }
}
