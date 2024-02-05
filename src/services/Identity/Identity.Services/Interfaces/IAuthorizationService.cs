using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;

namespace Identity.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<ResponseAuthenticatedDto> LoginAsync(RequestLoginUserDto loginUserDto, CancellationToken cancellationToken);
        Task<bool> RegistrationAsync(RequestRegistrationUserDto registrationUserDto, CancellationToken cancellationToken);
    }
}
