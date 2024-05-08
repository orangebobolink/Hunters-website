using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Interfaces;
using Mapster;
using MassTransit;
using Shared.Helpers;
using Shared.Messages.HunterLicenseMessage;

namespace Identity.Services.Services
{
    internal class HuntingLicenseService(
        IHyntingLicenseRepository hyntingLicenseRepository,
        IBus bus)
        : IHuntingLicenseService
    {
        private readonly IHyntingLicenseRepository _hyntingLicenseRepository = hyntingLicenseRepository;
        private readonly IBus _bus = bus;

        public async Task<HuntingLicenseResponseDto> CreateAsync(
            HuntingLicenseRequestDto huntingLicenseRequest,
            CancellationToken cancellationToken)
        {
            var existingHunterLicense = await _hyntingLicenseRepository.GetByPredicate(
                h => h.LicenseNumber == huntingLicenseRequest.LicenseNumber,
                cancellationToken);

            if (existingHunterLicense is not null)
            {
                throw new Exception();
            }

            var huntingLicense = huntingLicenseRequest.Adapt<HuntingLicense>();
            var id = Guid.NewGuid();
            huntingLicense.Id = id;
            huntingLicense.IssuedDate = DateTime.UtcNow;
            huntingLicense.ExpiryDate = DateTime.UtcNow.AddDays(10);
            _hyntingLicenseRepository.Create(huntingLicense);

            await _hyntingLicenseRepository.SaveChangesAsync(cancellationToken);

            var message = huntingLicense.Adapt<CreateHuntingLicense>();

            await _bus.Publish(message);

            var response = huntingLicense.Adapt<HuntingLicenseResponseDto>();

            return response;
        }

        public async Task<HuntingLicenseResponseDto> GetByLicenseNumberAsync(
            string licenseNumber,
            CancellationToken cancellationToken)
        {
            var existingHunterLicense = await _hyntingLicenseRepository.GetByPredicate(
                h => h.LicenseNumber == licenseNumber,
                cancellationToken);

            if (existingHunterLicense is null)
            {
                ThrowHelper.ThrowKeyNotFoundException(licenseNumber);
            }

            var response = existingHunterLicense.Adapt<HuntingLicenseResponseDto>();

            return response;
        }

        public async Task<HuntingLicenseResponseDto> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var existingHunterLicense = await _hyntingLicenseRepository.GetByPredicate(
                h => h.UserId == userId,
                cancellationToken);

            if (existingHunterLicense is null)
            {
                ThrowHelper.ThrowKeyNotFoundException(userId.ToString());
            }

            var response = existingHunterLicense.Adapt<HuntingLicenseResponseDto>();

            return response;
        }

        public Task<HuntingLicenseResponseDto> GetFromAnotherServiceAsync(
            string licenseNumber,
            CancellationToken cancellationToken)
        {
            var huntingLicenseResponseDto = new HuntingLicenseResponseDto
            {
                LicenseNumber = licenseNumber,
                IssuedDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(1)
            };

            return Task.FromResult(huntingLicenseResponseDto);
        }
    }
}