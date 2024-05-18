using Identity.Services.Dtos.ResponseDtos;

namespace Identity.Services.Interfaces
{
    public interface IVerificationService
    {
        Task<HuntingLicenseResponseDto> VerifyHuntingLicenseAsync(string licenseNumber, CancellationToken cancellationToken);
    }
}
