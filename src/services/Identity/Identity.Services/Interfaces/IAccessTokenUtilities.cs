using Identity.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Identity.Services.Interfaces
{
    public interface IAccessTokenUtilities
    {
        string ReadAccessTokeFromHeaders();
        string GetUsernameFromAccessToken();
        Task<JwtSecurityToken> GetTokenOptionsAsync(User user, CancellationToken cancellationToken);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
