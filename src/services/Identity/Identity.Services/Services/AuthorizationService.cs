﻿using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
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
        IHyntingLicenseRepository huntingLicenseRepository,
        IUserService userService,
        ILogger<AuthorizationService> logger) : IAuthorizationService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IHyntingLicenseRepository _huntingLicenseRepository = huntingLicenseRepository;
        private readonly IUserService _userService = userService;
        private readonly ILogger<AuthorizationService> _logger = logger;
        private readonly ThrowExceptionUtility<AuthorizationService> _throwExceptionUtilities = new(logger);

        public async Task<ResponseAuthenticatedDto> LoginAsync(RequestLoginUserDto loginUserDto,
            CancellationToken cancellationToken)
        {
            var user = await VerifyingTheValidityOfLoginDataAsync(loginUserDto);

            var accessToken = await _tokenService.GenerateAccessTokenAsync(user, cancellationToken);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _tokenService.UpdateUserRefreshTokenAsync(user, refreshToken);

            var huntingLicense = await _huntingLicenseRepository.GetByPredicate(
                hl => hl.UserId == user.Id
                    && hl.ExpiryDate > DateTime.Now,
                cancellationToken);

            if (huntingLicense is null)
            {
                huntingLicense = new HuntingLicense()
                {
                    IsPaid = false
                };
            }

            _logger.LogInformation($"User {user.UserName} logged in successfully.");

            var response = new ResponseAuthenticatedDto
            {
                Id = user.Id,
                UserName = user.UserName!,
                Roles = (List<string>)await _userManager.GetRolesAsync(user),
                AccessToken = accessToken,
                HuntingLicenseId = huntingLicense?.Id,
                IsPaid = huntingLicense.IsPaid,
                AvatarUrl = user.AvatarUrl
            };

            return response;
        }

        public async Task<bool> RegistrationAsync(RequestRegistrationUserDto registrationUserDto,
            CancellationToken cancellationToken)
        {
            var userRequestDto = registrationUserDto.Adapt<RequestUserDto>();
            userRequestDto.UserName = RandomUsernameGeneratorUtility.GenerateRandomUsername();
            userRequestDto.RoleNames = [Role.User];

            await _userService.CreateAsync(userRequestDto, cancellationToken);

            _logger.LogInformation($"User registered successfully. Username: {userRequestDto.UserName}");

            return true;
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