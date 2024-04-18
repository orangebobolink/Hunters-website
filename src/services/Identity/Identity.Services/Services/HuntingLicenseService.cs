using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Interfaces;
using Mapster;

namespace Identity.Services.Services
{
    internal class HuntingLicenseService(
        IHyntingLicenseRepository hyntingLicenseRepository)
        : IHuntingLicenseService
    {
        private readonly IHyntingLicenseRepository _hyntingLicenseRepository = hyntingLicenseRepository;

        public async Task<HuntingLicenseResponseDto> CreateAsync(
            HuntingLicenseRequestDto huntingLicenseRequest,
            CancellationToken cancellationToken)
        {
            if(huntingLicenseRequest.IssuedDate > DateTime.Now
              || huntingLicenseRequest.ExpiryDate < DateTime.Now)
            {
                throw new Exception();
            }

            var existingHunterLicense = await _hyntingLicenseRepository
                .GetByLicenseNumberAsync(huntingLicenseRequest.LicenseNumber, cancellationToken);

            if(existingHunterLicense is not null)
            {
                throw new Exception();
            }

            var huntingLicense = huntingLicenseRequest.Adapt<HuntingLicense>();
            var id = Guid.NewGuid();
            huntingLicense.Id = id;
            _hyntingLicenseRepository.Create(huntingLicense);

            await _hyntingLicenseRepository.SaveChangesAsync(cancellationToken);

            var response = huntingLicense.Adapt<HuntingLicenseResponseDto>();

            return response;
        }

        public async Task<HuntingLicenseResponseDto> GetByLicenseNumberAsync(
            string licenseNumber,
            CancellationToken cancellationToken)
        {
            var existingHunterLicense = await _hyntingLicenseRepository
                     .GetByLicenseNumberAsync(licenseNumber, cancellationToken);

            if(existingHunterLicense is null)
            {
                throw new Exception();
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