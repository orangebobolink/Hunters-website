using Identity.Domain.Entities;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Extentions;
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
        ILogger<AuthorizationService> logger) : IAuthorizationService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IUserService _userService = userService;
        private readonly ILogger<AuthorizationService> _logger = logger;
        private readonly ThrowExceptionUtilities<AuthorizationService> _throwExceptionUtilities = new(logger);

        public async Task<ResponseAuthenticatedDto> LoginAsync(RequestLoginUserDto loginUserDto,
            CancellationToken cancellationToken = default)
        {
            _ = loginUserDto
                ?? _throwExceptionUtilities.ThrowInvalidClientRequestException();

            var user = await VerifyingTheValidityOfLoginDataAsync(loginUserDto!);

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user, cancellationToken);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await UpdateUserRefreshTokenAsync(user, refreshToken);

            _logger.LogInformation($"User {user.UserName} logged in successfully.");

            return new ResponseAuthenticatedDto
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                User = user.Adapt<ResponseUserDto>(),
            };
        }

        public async Task<bool> RegistrationAsync(RequestRegistrationUserDto registrationUserDto,
            CancellationToken cancellationToken = default)
        {
            var userRequestDto = registrationUserDto.Adapt<RequestUserDto>();
            userRequestDto.UserName = RandomUsernameGeneratorUtility.GenerateRandomUsername();

            await _userService.CreateAsync(userRequestDto, cancellationToken);

            _logger.LogInformation($"User registered successfully. Username: {userRequestDto.UserName}");

            return true;
        }

        private async Task UpdateUserRefreshTokenAsync(User user, string refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            var userUpdateResult = await _userManager.UpdateAsync(user);

            userUpdateResult.CheckUserUpdateResult(_logger);
        }

        private async Task<User> VerifyingTheValidityOfLoginDataAsync(RequestLoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByNameAsync(loginUserDto.UserName)
                ?? _throwExceptionUtilities.ThrowAccountNotFoundException(loginUserDto.UserName);

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user!, loginUserDto.Password);

            isPasswordCorrect.CheckIsPasswordCorrect(_logger, user!.UserName!);

            return user;
        }
    }
}
