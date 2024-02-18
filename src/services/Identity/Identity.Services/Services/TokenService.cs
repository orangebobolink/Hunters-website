using Identity.Domain.Entities;
using Identity.Services.Dtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Extentions;
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
    ILogger<TokenService> logger) : ITokenService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly JwtUtilities _jwtUtilities = new(userManager, configuration);
        private readonly ILogger<TokenService> _logger = logger;
        private readonly ThrowExceptionUtilities<TokenService> _throwExceptionUtilities = new(logger);

        public async Task<string> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken)
        {
            var tokenOptions = await _jwtUtilities.GetTokenOptionsAsync(user, cancellationToken);
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

        public async Task<ResponseAuthenticatedDto> Refresh(Guid id, TokenApiDto tokenApiModel,
                                                         CancellationToken cancellationToken)
        {
            _ = tokenApiModel
                ?? _throwExceptionUtilities.ThrowInvalidTokenException();

            string refreshToken = tokenApiModel!.RefreshToken!;

            var user = (await _userManager.FindByIdAsync(id.ToString()))
                ?? _throwExceptionUtilities.ThrowAccountNotFoundException(id);

            user!.CheckUserRefreshToken(refreshToken, logger);

            var newAccessToken = await GenerateAccessTokenAsync(user!, cancellationToken);
            var newRefreshToken = GenerateRefreshToken();

            user!.RefreshToken = newRefreshToken;

            var userUpdateResult = await _userManager.UpdateAsync(user);

            userUpdateResult.CheckUserUpdateResult(_logger);

            return new ResponseAuthenticatedDto()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task Revoke(Guid id, CancellationToken cancellationToken)
        {
            var user = (await _userManager.FindByIdAsync(id.ToString()))
                ?? _throwExceptionUtilities.ThrowAccountNotFoundException(id);

            var userUpdateResult = await _userManager.UpdateAsync(user!);

            userUpdateResult.CheckUserUpdateResult(_logger);
        }

        private string GetRandomNumberGeneratorRefreshToken(byte[] randomNumber)
        {
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                string refreshToken = Convert.ToBase64String(randomNumber);

                _logger.LogInformation("Refresh token was created successfully");

                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}