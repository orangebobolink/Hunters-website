using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Extensions;
using Identity.Services.Interfaces;
using Identity.Services.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Identity.Services.Services
{
    public class TokenService(
        UserManager<User> userManager,
        ILogger<TokenService> logger,
        IHyntingLicenseRepository hyntingLicenseRepository,
        IAccessTokenUtilities accessTokenUtilities,
        IRefreshTokenUtilities refreshTokenUtilities,
        IRefreshTokenCookie refreshTokenCookieUtilities)
        : ITokenService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IHyntingLicenseRepository _hyntingLicenseRepository
            = hyntingLicenseRepository;
        private readonly IRefreshTokenCookie _refreshTokenCookieUtilities
            = refreshTokenCookieUtilities;
        private readonly IAccessTokenUtilities _accessTokenUtilities
            = accessTokenUtilities;
        private readonly IRefreshTokenUtilities _refreshTokenUtilities
            = refreshTokenUtilities;
        private readonly ILogger<TokenService> _logger = logger;
        private readonly ThrowExceptionUtility<TokenService> _throwExceptionUtilities
            = new(logger);

        public async Task<string> GenerateAccessTokenAsync(
            User user,
            CancellationToken cancellationToken)
        {
            JwtSecurityToken tokenOptions = await _accessTokenUtilities.GetTokenOptionsAsync(
                user,
                cancellationToken);

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

        public async Task<ResponseAuthenticatedDto> RefreshAsync(
            CancellationToken cancellationToken)
        {
            var username = _accessTokenUtilities.GetUsernameFromAccessToken();
            string refreshToken = _refreshTokenCookieUtilities.ReadRefreshTokenCookie();

            var user = await FindUserByUsernameAsync(username);
            user.CheckUserRefreshToken(refreshToken, _logger);

            var newAccessToken = await GenerateAccessTokenAsync(user!, cancellationToken);
            var newRefreshToken = GenerateRefreshToken();

            await UpdateUserRefreshTokenAsync(user, newRefreshToken);

            var huntingLicense = await _hyntingLicenseRepository.GetByPredicate(
                hl => hl.UserId == user.Id
                    && hl.ExpiryDate < DateTime.Now,
                cancellationToken);

            var isPaid = huntingLicense is not null;

            var response = new ResponseAuthenticatedDto()
            {
                Id = user.Id,
                UserName = user.UserName!,
                Roles = (List<string>)await _userManager.GetRolesAsync(user),
                IsPaid = isPaid,
                AccessToken = newAccessToken,
            };

            return response;
        }

        public async Task RevokeAsync(CancellationToken cancellationToken)
        {
            var username = _accessTokenUtilities.GetUsernameFromAccessToken();

            var user = await FindUserByUsernameAsync(username);

            await DeleteUserRefreshTokenAsync(user);
        }

        private string GetRandomNumberGeneratorRefreshToken(
            byte[] randomNumber)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                string refreshToken = Convert.ToBase64String(randomNumber);

                _logger.LogInformation("Refresh token was created successfully");

                return refreshToken;
            }
        }

        public async Task UpdateUserRefreshTokenAsync(
            User user,
            string newRefreshToken)
        {
            _refreshTokenUtilities.ChangeRefreshTokenForUser(user, newRefreshToken);

            var userUpdateResult = await _userManager.UpdateAsync(user);

            userUpdateResult.CheckUserUpdateResult(_logger);

            _refreshTokenCookieUtilities.AddRefreshTokenCookie(newRefreshToken);
        }

        public async Task DeleteUserRefreshTokenAsync(User user)
        {
            user.RefreshToken = string.Empty;

            var userUpdateResult = await _userManager.UpdateAsync(user!);

            userUpdateResult.CheckUserUpdateResult(_logger);

            _refreshTokenCookieUtilities.DeleteRefreshTokenCookie();
        }


        private async Task<User> FindUserByUsernameAsync(string username)
        {
            User? user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                _throwExceptionUtilities.ThrowAccountNotFoundException(username);
            }

            return user!;
        }
    }
}