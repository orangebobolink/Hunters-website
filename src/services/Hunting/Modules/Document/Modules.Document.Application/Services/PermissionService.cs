using Mapster;
using Microsoft.Extensions.Logging;
using Modules.Document.Application.Dtos.RequestDtos;
using Modules.Document.Application.Dtos.ResponseDtos;
using Modules.Document.Application.Interfaces;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Shared.Helpers;
using System.Linq;

namespace Modules.Document.Application.Services
{
    internal class PermissionService(
        IPermissionForExtractionOfHuntingAnimalRepository permissionRepository,
        IAnimalRepository animalRepository,
        ICouponRepository couponRepository,
        ILogger<PermissionService> logger)
        : IPermissionService
    {
        private readonly IPermissionForExtractionOfHuntingAnimalRepository _permissionRepository
            = permissionRepository;
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly ICouponRepository _couponRepository = couponRepository;
        private readonly ILogger<PermissionService> _logger = logger;

        public async Task<PermissionResponseDto> CreateAsync(
            PermisionRequestDto request,
            CancellationToken cancellationToken)
        {
            var existingPermission = await _permissionRepository.GetByPredicate(
                p => p.Number == request.Number,
                cancellationToken);

            if (existingPermission is not null)
            {
                _logger.LogWarning("id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(existingPermission));
            }

            var permission = request.Adapt<PermissionForExtractionOfHuntingAnimal>();
            permission.Id = Guid.NewGuid();

            if (request.Animal is null)
            {
                permission.Animal = await _animalRepository.GetByPredicate(
                    a => a.Id == request.AnimalId,
                    cancellationToken);
            }

            permission.Coupons = Enumerable.Range(0, request.NumberOfCoupons)
                                .Select(i => new Coupon
                                {
                                    AnimalName = permission.Animal?.Name!
                                })
                                .ToList();
            permission.Animal = null;
            _permissionRepository.Create(permission!);

            await _permissionRepository.SaveChangesAsync(cancellationToken);

            var response = permission.Adapt<PermissionResponseDto>();

            return response;
        }

        public async Task<PermissionResponseDto> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var existingPermission = await _permissionRepository.GetByPredicate(
                e => e.Id == id, cancellationToken);

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

        public async Task<List<PermissionResponseDto>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAllAsync(
                cancellationToken);

            var response = permissions.Adapt<List<PermissionResponseDto>>();

            foreach (var item in response)
            {
                item.NumberOfCoupons = item.Coupons
                    .Where(c => !c.IsUsed)
                    .Count();
            }

            return response;
        }

        public async Task<List<PermissionResponseDto>> GetAllIncludeAsync(
            CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAllIncludeAsync(
                cancellationToken);

            var response = permissions.Adapt<List<PermissionResponseDto>>();

            foreach (var item in response)
            {
                item.NumberOfCoupons = item.Coupons
                    .Where(c => !c.IsUsed)
                    .Count();
            }

            return response;
        }

        public async Task<PermissionResponseDto> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByPredicate(
                e => e.Id == id, cancellationToken);

            if (permission is null)
            {
                _logger.LogWarning("Id is null");
                ThrowHelper.ThrowKeyNotFoundException(nameof(id));
            }

            var response = permission.Adapt<PermissionResponseDto>();

            return response;
        }

        public async Task<PermissionResponseDto> GetByIdIncludeAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdIncludeAsync(
                id, cancellationToken);

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
            var existingPermission = await _permissionRepository.GetByPredicate(
                e => e.Id == id, cancellationToken);

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
