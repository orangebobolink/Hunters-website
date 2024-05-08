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
    internal class TripService(
        ITripRepository tripRepository,
        ILogger<TripService> logger)
        : ITripService
    {
        private readonly ITripRepository _tripRepository = tripRepository;
        private readonly ILogger<TripService> _logger = logger;

        public async Task<TripResponseDto> CreateAsync(
            TripRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingTrip = await _tripRepository.GetByPredicate(
                t => t.Number == request.Number,
                cancellationToken);

            if (existingTrip is not null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingTrip));
            }

            var trip = request.Adapt<Trip>();
            trip.Id = Guid.NewGuid();

            _tripRepository.Create(trip!);

            await _tripRepository.SaveChangesAsync(cancellationToken);

            var response = trip.Adapt<TripResponseDto>();

            return response;
        }

        public async Task<TripResponseDto> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var existingTrip = await _tripRepository.GetByPredicate(
                e => e.Id == id, cancellationToken);

            if (existingTrip is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingTrip));
            }

            _tripRepository.Delete(existingTrip!);

            await _tripRepository.SaveChangesAsync(cancellationToken);

            var response = existingTrip.Adapt<TripResponseDto>();

            return response;
        }

        public async Task<List<TripResponseDto>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var permissions = await _tripRepository.GetAllAsync(cancellationToken);

            var response = permissions.Adapt<List<TripResponseDto>>();

            return response;
        }

        public async Task<List<TripResponseDto>> GetAllIncludeAsync(
            CancellationToken cancellationToken)
        {
            var permissions = await _tripRepository.GetAllIncludeAsync(cancellationToken);

            var response = permissions.Adapt<List<TripResponseDto>>();

            return response;
        }

        public async Task<TripResponseDto> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var permission = await _tripRepository.GetByPredicate(
                e => e.Id == id, cancellationToken);

            if (permission is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = permission.Adapt<TripResponseDto>();

            return response;
        }

        public async Task<TripResponseDto> GetByIdIncludeAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var permission = await _tripRepository.GetByIdIncludeAsync(id, cancellationToken);

            if (permission is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = permission.Adapt<TripResponseDto>();

            return response;
        }

        public async Task<TripResponseDto> GetByParticipantId(
            Guid participantId,
            CancellationToken cancellationToken)
        {
            var permission = await _tripRepository.GetByPredicate(
                e => e.TripParticipants.Any(p => p.Id == participantId),
                cancellationToken);

            if (permission is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(participantId));
            }

            var response = permission.Adapt<TripResponseDto>();

            return response;
        }

        public async Task<TripResponseDto> UpdateAsync(
            Guid id,
            TripRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingTrip = await _tripRepository.GetByPredicate(
                e => e.Id == id, cancellationToken);

            if (existingTrip is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingTrip));
            }

            var permission = request.Adapt(existingTrip);

            _tripRepository.Update(permission!);

            await _tripRepository.SaveChangesAsync(cancellationToken);

            var response = existingTrip.Adapt<TripResponseDto>();

            return response;
        }
    }
}
