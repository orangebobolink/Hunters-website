using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services.Utilities
{
    internal class JwtUtilities(UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration)
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IConfiguration _configuration = configuration;

        private void SetRefreshTokenCookie(string newRefreshToken)
        {
            var refreshTokenCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["JWT:RefreshTokenValidityInDays"])),
                Path = "/",
            };

            _httpContextAccessor.HttpContext?.Response
                .Cookies.Append("RefreshToken", newRefreshToken, refreshTokenCookieOptions);
        }

        public async Task<JwtSecurityToken> GetTokenOptionsAsync(User user, CancellationToken cancellationToken)
        {
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var claims = await GetClaimsAsync(user, cancellationToken);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            int expiryTimeToken = GetExpiryTimeToken();

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryTimeToken),
                signingCredentials: signinCredentials
            );

            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claims.AddRange(roleClaims);

            return claims;
        }

        private int GetExpiryTimeToken()
        {
            string expiryTimeTokenString = _configuration["JwtSettings:ExpiresInMinute"]!;

            if(!int.TryParse(expiryTimeTokenString, out int expiryTimeTokenInt))
            {
                throw new InvalidConfigurationException();
            }

            return expiryTimeTokenInt;
        }
    }
}
