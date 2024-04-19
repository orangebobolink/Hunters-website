using Identity.Domain.Entities;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Identity.Services.Utilities
{
    internal class RefreshTokenUtility(ICookieUtilities cookieUtilities,
        IConfiguration configuration)
        : IRefreshTokenCookie, IRefreshTokenUtilities
    {
        private readonly ICookieUtilities _cookieUtilities = cookieUtilities;
        private readonly IConfiguration _configuration = configuration;
        private readonly string _cookieRefreshTokenKey = configuration["JwtSettings:RefreshToken:CookieName"]!;

        public void AddRefreshTokenCookie(string newRefreshToken)
        {
            var expiresInDay = int.Parse(_configuration["JwtSettings:RefreshToken:ExpiresInDay"]!);

            var refreshTokenCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(expiresInDay),
                Path = "/",
            };

            _cookieUtilities.AddToCookie(_cookieRefreshTokenKey, newRefreshToken, refreshTokenCookieOptions);
        }

        public string ReadRefreshTokenCookie()
        {
            var data = _cookieUtilities.ReadFromCookie(_cookieRefreshTokenKey);

            return data!;
        }

        public void DeleteRefreshTokenCookie()
        {
            _cookieUtilities.RemoveFromCookie(_cookieRefreshTokenKey);
        }

        public void ChangeRefreshTokenForUser(User user, string newRefreshToken)
        {
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now
                                    .AddDays(int.Parse(_configuration["JwtSettings:RefreshToken:ExpiresInDay"]!));
        }
    }
}
