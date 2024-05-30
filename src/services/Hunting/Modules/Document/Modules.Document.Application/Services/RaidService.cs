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
    internal class RaidService(
        IRaidRepository raidRepository,
        IUserRepository userRepository,
        ILogger<RaidService> logger)
        : IRaidService
    {
        private readonly IRaidRepository _raidRepository = raidRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<RaidService> _logger = logger;

        public async Task<RaidResponseDto> CreateAsync(
            RaidRequestDto request,
            CancellationToken cancellationToken)
        {
            if (request.Participants.Count == 0)
            {
                _logger.LogWarning("participants is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(request));
            }

            var existingRaid = await _raidRepository.GetByPredicate(
                r => r.ExitTime == request.ExitTime
                && r.ReturnedTime == request.ReturnedTime,
                cancellationToken);

            if (existingRaid is not null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingRaid));
            }

            var raid = request.Adapt<Raid>();
            raid.Participants = await _userRepository.GetAllExistsUsers(
                raid.Participants.ToList());
            raid.Id = Guid.NewGuid();

            _raidRepository.Create(raid!);

            await _raidRepository.SaveChangesAsync(cancellationToken);

            var response = raid.Adapt<RaidResponseDto>();

            return response;
        }

        public async Task<RaidResponseDto> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var existingRaid = await _raidRepository.GetByPredicate(e => e.Id == id, cancellationToken);

            if (existingRaid is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingRaid));
            }

            _raidRepository.Delete(existingRaid!);

            await _raidRepository.SaveChangesAsync(cancellationToken);

            var response = existingRaid.Adapt<RaidResponseDto>();

            return response;
        }

        public async Task<List<RaidResponseDto>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var permissions = await _raidRepository.GetAllAsync(cancellationToken);

            var response = permissions.Adapt<List<RaidResponseDto>>();

            return response;
        }

        public async Task<List<RaidResponseDto>> GetAllIncludeAsync(
            CancellationToken cancellationToken)
        {
            var permissions = await _raidRepository.GetAllIncludeAsync(cancellationToken);

            var response = permissions.Adapt<List<RaidResponseDto>>();

            return response;
        }

        public async Task<RaidResponseDto> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var permission = await _raidRepository.GetByPredicate(e => e.Id == id, cancellationToken);

            if (permission is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = permission.Adapt<RaidResponseDto>();

            return response;
        }

        public async Task<RaidResponseDto> GetByIdIncludeAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var permission = await _raidRepository.GetByIdIncludeAsync(id, cancellationToken);

            if (permission is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = permission.Adapt<RaidResponseDto>();

            return response;
        }

        public async Task<List<RaidResponseDto>> GetRaidsByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var raids = await _raidRepository.GetRaidsByIdAsync(id, cancellationToken);

            if (raids is { Count: 0 })
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = raids.Adapt<List<RaidResponseDto>>();

            return response;
        }

        public async Task<RaidResponseDto> UpdateAsync(
            Guid id,
            RaidRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingRaid = await _raidRepository.GetByPredicate(e => e.Id == id, cancellationToken);

            if (existingRaid is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingRaid));
            }

            var permission = request.Adapt(existingRaid);

            _raidRepository.Update(permission!);

            await _raidRepository.SaveChangesAsync(cancellationToken);

            var response = existingRaid.Adapt<RaidResponseDto>();

            return response;
        }
    }
}
