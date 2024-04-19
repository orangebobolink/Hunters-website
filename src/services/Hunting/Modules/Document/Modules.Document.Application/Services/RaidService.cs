﻿using Mapster;
using Microsoft.Extensions.Logging;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDto;
using Modules.Document.Application.Interfaces;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Shared.Helpers;

namespace Modules.Document.Application.Services
{
    internal class RaidService(IRaidRepository raidRepository, ILogger<RaidService> logger) : IRaidService
    {
        private readonly IRaidRepository _raidRepository = raidRepository;
        private readonly ILogger<RaidService> _logger = logger;

        public async Task<RaidResponseDto> CreateAsync(RaidRequestDto request, CancellationToken cancellationToken)
        {
            //var existingFeeding = await _feedingRepository.GetByIdAsync(id, cancellationToken);

            //if (existingFeedingProduct is null)
            //{
            //    _logger.LogWarning("id is null");
            //    ThrowHelper.ThrowKeyNotFoundException(nameof(existingFeedingProduct));
            //}

            var raid = request.Adapt<Raid>();
            raid.Id = Guid.NewGuid();

            _raidRepository.Create(raid!);

            await _raidRepository.SaveChangesAsync(cancellationToken);

            var response = raid.Adapt<RaidResponseDto>();

            return response;
        }

        public async Task<RaidResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingRaid = await _raidRepository.GetByIdAsync(id, cancellationToken);

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

        public async Task<List<RaidResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var permissions = await _raidRepository.GetAllAsync(cancellationToken);

            var response = permissions.Adapt<List<RaidResponseDto>>();

            return response;
        }

        public async Task<List<RaidResponseDto>> GetAllIncludeAsync(CancellationToken cancellationToken)
        {
            var permissions = await _raidRepository.GetAllIncludeAsync(cancellationToken);

            var response = permissions.Adapt<List<RaidResponseDto>>();

            return response;
        }

        public async Task<RaidResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var permission = await _raidRepository.GetByIdAsync(id, cancellationToken);

            if (permission is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = permission.Adapt<RaidResponseDto>();

            return response;
        }

        public async Task<RaidResponseDto?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken)
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

        public async Task<RaidResponseDto> UpdateAsync(Guid id, RaidRequestDto request, CancellationToken cancellationToken)
        {
            var existingRaid = await _raidRepository.GetByIdAsync(id, cancellationToken);

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
