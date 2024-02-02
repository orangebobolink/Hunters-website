using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Identity.Services.Dtos;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Interfaces;
using Identity.Services.Utilities;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services.Services
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthorizationService(UserManager<User> userManager,
            ITokenService tokenService,
            IUserService userService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userService = userService;
        }

        public async Task<AuthenticatedResponse> LoginAsync(LoginUserDto loginUserDto, CancellationToken cancellationToken = default)
        {
            if(loginUserDto is null)
            {
                throw new InvalidClientRequestException();
            }

            var user = await _userManager.FindByNameAsync(loginUserDto.UserName);

            if(user is null)
            {
                throw new UnauthorizedAccessException();
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if(!checkPassword)
            {

            }

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user, cancellationToken);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded == false)
                throw new Exception();

            return new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                User = user.Adapt<ResponseUserDto>(),
            };
        }

        public async Task<bool> RegistrationAsync(RegistrationUserDto registrationUserDto, CancellationToken cancellationToken = default)
        {
            var userRequestDto = registrationUserDto.Adapt<RequestUserDto>();
            userRequestDto.UserName = RandomUsernameGeneratorUtility.GenerateRandomUsername();

            var response = await _userService.CreateAsync(userRequestDto, cancellationToken);

            return true;
        }
    }
}
