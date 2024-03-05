using Identity.Domain.Entities;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Extensions;
using Identity.Services.Interfaces;
using Identity.Services.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Identity.Services.Services
{
    public class TokenService(UserManager<User> userManager,
        IConfiguration configuration,
        ILogger<TokenService> logger,
        IAccessTokenUtilities accessTokenUtilities,
        IRefreshTokenUtilities refreshTokenUtilities,
        IRefreshTokenCookie refreshTokenCookieUtilities) : ITokenService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly IRefreshTokenCookie _refreshTokenCookieUtilities = refreshTokenCookieUtilities;
        private readonly IAccessTokenUtilities _accessTokenUtilities = accessTokenUtilities;
        private readonly IRefreshTokenUtilities _refreshTokenUtilities = refreshTokenUtilities;
        private readonly ILogger<TokenService> _logger = logger;
        private readonly ThrowExceptionUtilities<TokenService> _throwExceptionUtilities = new(logger);

        public async Task<string> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken)
        {
            JwtSecurityToken tokenOptions = await _accessTokenUtilities.GetTokenOptionsAsync(user, cancellationToken);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            _logger.LogInformation("Access token was created successfully");

            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            string refreshToken = GetRandomNumberGeneratorRefreshToken(randomNumber);

            _logger.LogInformation("Refresh token was created successfully");

            return refreshToken;
        }

        public async Task<ResponseAuthenticatedDto> RefreshAsync(CancellationToken cancellationToken)
        {
            var username = _accessTokenUtilities.GetNameFromAccessToken();

            string refreshToken = _refreshTokenCookieUtilities.ReadRefreshTokenCookie();

            User user = (await _userManager.FindByNameAsync(username))
                ?? _throwExceptionUtilities.ThrowAccountNotFoundException(username);

            user.CheckUserRefreshToken(refreshToken, _logger);

            var newAccessToken = await GenerateAccessTokenAsync(user!, cancellationToken);
            var newRefreshToken = GenerateRefreshToken();

            _refreshTokenUtilities.UpdateRefreshTokenForUser(user, newRefreshToken);

            IdentityResult userUpdateResult = await _userManager.UpdateAsync(user);

            userUpdateResult.CheckUserUpdateResult(_logger);

            _refreshTokenCookieUtilities.AddRefreshTokenCookie(newRefreshToken);

            var response = new ResponseAuthenticatedDto()
            {
                AccessToken = newAccessToken,
            };

            return response;
        }

        public async Task RevokeAsync(CancellationToken cancellationToken)
        {
            var username = _accessTokenUtilities.GetNameFromAccessToken();

            var user = (await _userManager.FindByNameAsync(username))
                ?? _throwExceptionUtilities.ThrowAccountNotFoundException(username);

            user.RefreshToken = string.Empty;

            var userUpdateResult = await _userManager.UpdateAsync(user!);

            userUpdateResult.CheckUserUpdateResult(_logger);

            _refreshTokenCookieUtilities.DeleteRefreshTokenCookie();
        }

        private string GetRandomNumberGeneratorRefreshToken(byte[] randomNumber)
        {
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                string refreshToken = Convert.ToBase64String(randomNumber);

                _logger.LogInformation("Refresh token was created successfully");

                return refreshToken;
            }
        }
    }
}