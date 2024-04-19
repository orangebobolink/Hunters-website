using Mapster;
using Microsoft.Extensions.Logging;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDto;
using Modules.Document.Application.Interfaces;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Shared.Helpers;

namespace Modules.Document.Application.Services
{
    internal class TripParticipantService(ITripParticipantRepository tripParticipantRepository, ILogger<TripParticipantService> logger) : ITripParticipantService
    {
        private readonly ITripParticipantRepository _tripParticipantRepository = tripParticipantRepository;
        private readonly ILogger<TripParticipantService> _logger = logger;

        public async Task<TripResponseDto> CreateAsync(TripRequestDto request, CancellationToken cancellationToken)
        {
            //var existingFeeding = await _feedingRepository.GetByIdAsync(id, cancellationToken);

            //if (existingFeedingProduct is null)
            //{
            //    _logger.LogWarning("id is null");
            //    ThrowHelper.ThrowKeyNotFoundException(nameof(existingFeedingProduct));
            //}

            var tripParticipant = request.Adapt<TripParticipant>();
            tripParticipant.Id = Guid.NewGuid();

            _tripParticipantRepository.Create(tripParticipant!);

            await _tripParticipantRepository.SaveChangesAsync(cancellationToken);

            var response = tripParticipant.Adapt<TripResponseDto>();

            return response;
        }

        public async Task<TripResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingTripParticipant = await _tripParticipantRepository.GetByIdAsync(id, cancellationToken);

            if (existingTripParticipant is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingTripParticipant));
            }

            _tripParticipantRepository.Delete(existingTripParticipant!);

            await _tripParticipantRepository.SaveChangesAsync(cancellationToken);

            var response = existingTripParticipant.Adapt<TripResponseDto>();

            return response;
        }

        public async Task<TripResponseDto> UpdateAsync(Guid id, TripRequestDto request, CancellationToken cancellationToken)
        {
            var existingTripParticipant = await _tripParticipantRepository.GetByIdAsync(id, cancellationToken);

            if (existingTripParticipant is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingTripParticipant));
            }

            var permission = request.Adapt(existingTripParticipant);

            _tripParticipantRepository.Update(permission!);

            await _tripParticipantRepository.SaveChangesAsync(cancellationToken);

            var response = existingTripParticipant.Adapt<TripResponseDto>();

            return response;
        }
    }
}
