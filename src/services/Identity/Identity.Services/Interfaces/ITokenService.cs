using Identity.Domain.Entities;
using Identity.Services.Dtos;
using Identity.Services.Dtos.ResponseDtos;

namespace Identity.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken);
        string GenerateRefreshToken();
        Task<ResponseAuthenticatedDto> RefreshAsync(CancellationToken cancellationToken);
        Task RevokeAsync(CancellationToken cancellationToken);
        Task UpdateUserRefreshTokenAsync(User user, string newRefreshToken);
        Task DeleteUserRefreshTokenAsync(User user);
    }
}
