using Identity.Domain.Entities;
using Identity.Services.Dtos;
using Identity.Services.Dtos.ResponseDtos;

namespace Identity.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken);
        string GenerateRefreshToken();
        Task<ResponseAuthenticatedDto> Refresh(Guid id, TokenApiDto tokenApiModel, CancellationToken cancellationToken);
        Task Revoke(Guid id, CancellationToken cancellationToken);
    }
}
