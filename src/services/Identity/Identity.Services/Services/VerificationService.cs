using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Interfaces;
using Mapster;

namespace Identity.Services.Services
{
    internal class VerificationService(
        IHuntingLicenseService huntingLicenseService)
        : IVerificationService
    {
        private readonly IHuntingLicenseService _huntingLicenseService = huntingLicenseService;

        public async Task<HuntingLicenseResponseDto> VerifyHuntingLicenseAsync(
            string licenseNumber,
            CancellationToken cancellationToken)
        {
            var huntingLicenseFromService = await _huntingLicenseService.GetFromAnotherServiceAsync(
                licenseNumber,
                cancellationToken);

            if (huntingLicenseFromService is null)
            {
                throw new Exception();
            }

            var huntingLicense = huntingLicenseFromService.Adapt<HuntingLicenseRequestDto>();

            var resposne = await _huntingLicenseService.CreateAsync(huntingLicense, cancellationToken);

            return resposne;
        }
    }
}
