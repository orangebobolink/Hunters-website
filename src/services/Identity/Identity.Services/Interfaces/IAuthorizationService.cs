using Identity.Services.Dtos;

namespace Identity.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<AuthenticatedResponse> LoginAsync(LoginUserDto loginUserDto, CancellationToken cancellationToken);
        Task<bool> RegistrationAsync(RegistrationUserDto registrationUserDto, CancellationToken cancellationToken);
    }
}
