using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Identity.Services.Dtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Extentions;
using Identity.Services.Interfaces;
using Identity.Services.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Services.Services
{
    public class TokenService(UserManager<User> userManager,
    IConfiguration configuration,
    ILogger<TokenService> logger) : ITokenService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly JwtUtilities _jwtUtilities = new(userManager, configuration);
        private readonly ILogger<TokenService> _logger = logger;

        public async Task<string> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken = default)
        {
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            var claims = await _jwtUtilities.GetClaimsAsync(user, cancellationToken);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            int expiryTimeToken = _jwtUtilities.GetExpiryTimeToken();

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryTimeToken),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            _logger.LogInformation("Access token was created successfully");

            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                _logger.LogInformation("Refresh token was created successfully");

                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<ResponseAuthenticatedDto> Refresh(Guid id, TokenApiDto tokenApiModel,
                                                         CancellationToken cancellationToken = default)
        {
            if(tokenApiModel is null)
            {
                _logger.LogError("Token api is null");

                throw new InvalidTokenException();
            }

            string refreshToken = tokenApiModel.RefreshToken!;

            var user = await _userManager.FindByIdAsync(id.ToString());

            NullCheckerUtilities.CheckUserExistence(user, logger, id);

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

        public async Task Revoke(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            NullCheckerUtilities.CheckUserExistence(user, _logger, id);

            var userUpdateResult = await _userManager.UpdateAsync(user!);

            userUpdateResult.CheckUserUpdateResult(_logger);
        }
    }
}