using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Interfaces;
using Identity.Services.Utilities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Services.Services
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthorizationService> _logger;

        public AuthorizationService(UserManager<User> userManager, ITokenService tokenService,
            IUserService userService, ILogger<AuthorizationService> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userService = userService;
            _logger = logger;
        }

        public async Task<ResponseAuthenticatedDto> LoginAsync(RequestLoginUserDto loginUserDto, CancellationToken cancellationToken = default)
        {
            if(loginUserDto is null)
            {
                _logger.LogError("Invalid client request: loginUserDto is null.");

                throw new InvalidClientRequestException();
            }

            var user = await _userManager.FindByNameAsync(loginUserDto.UserName);

            if(user is null)
            {
                _logger.LogError($"User not found for username: {loginUserDto.UserName}.");

                throw new UnauthorizedAccessException();
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if(!checkPassword)
            {
                _logger.LogError($"Incorrect password for user: {user.UserName}.");

                throw new InvalidPasswordException();
            }

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user, cancellationToken);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded == false)
            {
                _logger.LogError($"Failed to update user: {user.UserName}.");

                throw new Exception();
            }

            _logger.LogInformation($"User {user.UserName} logged in successfully.");

            return new ResponseAuthenticatedDto
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                User = user.Adapt<ResponseUserDto>(),
            };
        }

        public async Task<bool> RegistrationAsync(RequestRegistrationUserDto registrationUserDto, CancellationToken cancellationToken = default)
        {
            var userRequestDto = registrationUserDto.Adapt<RequestUserDto>();
            userRequestDto.UserName = RandomUsernameGeneratorUtility.GenerateRandomUsername();

            var response = await _userService.CreateAsync(userRequestDto, cancellationToken);

            _logger.LogInformation($"User registered successfully. Username: {userRequestDto.UserName}");

            return true;
        }
    }
}
