using Identity.Domain.Entities;
using Identity.Services.Dtos;

namespace Identity.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken = default);
        string GenerateRefreshToken();
        Task<AuthenticatedResponse> Refresh(Guid id, TokenApiDto tokenApiModel, CancellationToken cancellationToken = default);
        Task Revoke(Guid id, CancellationToken cancellationToken = default);
    }
}
