using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;

namespace Identity.Services.Interfaces
{
    public interface IHuntingLicenseService
    {
        Task<HuntingLicenseResponseDto> GetByLicenseNumberAsync(
            string licenseNumber, 
            CancellationToken cancellationToken);
        Task<HuntingLicenseResponseDto> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken);
        Task<HuntingLicenseResponseDto> CreateAsync(
            HuntingLicenseRequestDto huntingLicenseRequest,
            CancellationToken cancellationToken);
        Task<HuntingLicenseResponseDto> GetFromAnotherServiceAsync(
            string licenseNumber, 
            CancellationToken cancellationToken);
    }
}
