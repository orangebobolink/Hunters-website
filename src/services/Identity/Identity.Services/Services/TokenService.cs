using Identity.Domain.Entities;
using Identity.Services.Dtos;
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
    ILogger<TokenService> logger) : ITokenService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly JwtUtilities _jwtUtilities = new(userManager, configuration);
        private readonly ILogger<TokenService> _logger = logger;
        private readonly ThrowExceptionUtilities<TokenService> _throwExceptionUtilities = new(logger);

        public async Task<string> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken)
        {
            JwtSecurityToken tokenOptions = await _jwtUtilities.GetTokenOptionsAsync(user, cancellationToken);
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
            string refreshToken = tokenApiModel.RefreshToken!;

            User user = (await _userManager.FindByIdAsync(id.ToString()))
                ?? _throwExceptionUtilities.ThrowAccountNotFoundException(id);

            user!.CheckUserRefreshToken(refreshToken, _logger);

            var newAccessToken = await GenerateAccessTokenAsync(user!, cancellationToken);
            var newRefreshToken = GenerateRefreshToken();

            user!.RefreshToken = newRefreshToken;

            IdentityResult userUpdateResult = await _userManager.UpdateAsync(user);

            userUpdateResult.CheckUserUpdateResult(_logger);

            var response = new ResponseAuthenticatedDto()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return response;
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

                return refreshToken;
            }
        }
    }
}