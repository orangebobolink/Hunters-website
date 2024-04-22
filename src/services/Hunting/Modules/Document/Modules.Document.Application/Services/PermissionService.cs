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
    internal class PermissionService(
        IPermissionForExtractionOfHuntingAnimalRepository permissionRepository,
        ILogger<PermissionService> logger)
        : IPermissionService
    {
        private readonly IPermissionForExtractionOfHuntingAnimalRepository _permissionRepository = permissionRepository;
        private readonly ILogger<PermissionService> _logger = logger;

        public async Task<PermissionResponseDto> CreateAsync(PermisionRequestDto request, CancellationToken cancellationToken)
        {
            //var existingFeeding = await _feedingRepository.GetByIdAsync(id, cancellationToken);

            //if (existingFeedingProduct is null)
            //{
            //    _logger.LogWarning("id is null");
            //    ThrowHelper.ThrowKeyNotFoundException(nameof(existingFeedingProduct));
            //}

            var permision = request.Adapt<PermissionForExtractionOfHuntingAnimal>();
            permision.Id = Guid.NewGuid();

            _permissionRepository.Create(permision!);

            await _permissionRepository.SaveChangesAsync(cancellationToken);

            var response = permision.Adapt<PermissionResponseDto>();

            return response;
        }

        public async Task<PermissionResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingPermission = await _permissionRepository.GetByPredicate(e => e.Id == id, cancellationToken);

            if (existingPermission is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingPermission));
            }

            _permissionRepository.Delete(existingPermission!);

            await _permissionRepository.SaveChangesAsync(cancellationToken);

            var response = existingPermission.Adapt<PermissionResponseDto>();

            return response;
        }

        public async Task<List<PermissionResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAllAsync(cancellationToken);

            var response = permissions.Adapt<List<PermissionResponseDto>>();

            return response;
        }

        public async Task<List<PermissionResponseDto>> GetAllIncludeAsync(CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAllIncludeAsync(cancellationToken);

            var response = permissions.Adapt<List<PermissionResponseDto>>();

            return response;
        }

        public async Task<PermissionResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByPredicate(e => e.Id == id, cancellationToken);

            if (permission is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = permission.Adapt<PermissionResponseDto>();

            return response;
        }

        public async Task<PermissionResponseDto?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdIncludeAsync(id, cancellationToken);

            if (permission is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = permission.Adapt<PermissionResponseDto>();

            return response;
        }

        public async Task<PermissionResponseDto> UpdateAsync(
            Guid id,
            PermisionRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingPermission = await _permissionRepository.GetByPredicate(e => e.Id == id, cancellationToken);

            if (existingPermission is null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingPermission));
            }

            var permission = request.Adapt(existingPermission);

            _permissionRepository.Update(permission!);

            await _permissionRepository.SaveChangesAsync(cancellationToken);

            var response = existingPermission.Adapt<PermissionResponseDto>();

            return response;
        }
    }
}
