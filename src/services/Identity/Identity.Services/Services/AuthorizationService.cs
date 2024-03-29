using Identity.Domain.Entities;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Extensions;
using Identity.Services.Interfaces;
using Identity.Services.Utilities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Services.Services
{
    internal class AuthorizationService(UserManager<User> userManager,
        ITokenService tokenService,
        IUserService userService,
        IRefreshTokenCookie refreshTokenCookieUtilities,
        IRefreshTokenUtilities refreshTokenUtilities,
        ILogger<AuthorizationService> logger) : IAuthorizationService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IUserService _userService = userService;
        private readonly ILogger<AuthorizationService> _logger = logger;
        private readonly ThrowExceptionUtilities<AuthorizationService> _throwExceptionUtilities = new(logger);
        private readonly IRefreshTokenCookie _refreshTokenCookieUtilities = refreshTokenCookieUtilities;
        private readonly IRefreshTokenUtilities _refreshTokenUtilities = refreshTokenUtilities;

        public async Task<ResponseAuthenticatedDto> LoginAsync(RequestLoginUserDto loginUserDto,
            CancellationToken cancellationToken)
        {
            var user = await VerifyingTheValidityOfLoginDataAsync(loginUserDto);

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user, cancellationToken);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await UpdateUserRefreshTokenAsync(user, refreshToken);

            _logger.LogInformation($"User {user.UserName} logged in successfully.");

            var response = new ResponseAuthenticatedDto
            {
                Id = user.Id,
                UserName = user.UserName!,
                Roles = (List<string>)await _userManager.GetRolesAsync(user),
                AccessToken = accessToken,
            };

            return response;
        }

        public async Task<bool> RegistrationAsync(RequestRegistrationUserDto registrationUserDto,
            CancellationToken cancellationToken)
        {
            var userRequestDto = registrationUserDto.Adapt<RequestUserDto>();
            userRequestDto.UserName = RandomUsernameGeneratorUtility.GenerateRandomUsername();

            await _userService.CreateAsync(userRequestDto, cancellationToken);

            _logger.LogInformation($"User registered successfully. Username: {userRequestDto.UserName}");

            return true;
        }

        private async Task UpdateUserRefreshTokenAsync(User user, string refreshToken)
        {
            _refreshTokenUtilities.UpdateRefreshTokenForUser(user, refreshToken);

            var userUpdateResult = await _userManager.UpdateAsync(user);

            userUpdateResult.CheckUserUpdateResult(_logger);

            _refreshTokenCookieUtilities.AddRefreshTokenCookie(refreshToken);
        }

        private async Task<User> VerifyingTheValidityOfLoginDataAsync(RequestLoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDto.Email)
                ?? _throwExceptionUtilities.ThrowAccountNotFoundException(loginUserDto.Email);

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user!, loginUserDto.Password);

            isPasswordCorrect.CheckIsPasswordCorrect(_logger, user!.UserName!);

            return user;
        }
    }
}
