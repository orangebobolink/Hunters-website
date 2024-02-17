using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Extentions;
using Identity.Services.Interfaces;
using Identity.Services.Utilities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MR.AspNetCore.Pagination;

namespace Identity.Services.Services
{
    internal class UserService(UserManager<User> userManager,
        IPaginationService paginationService,
        ILogger<UserService> logger) : IUserService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IPaginationService _paginationService = paginationService;
        private readonly ILogger<UserService> _logger = logger;

        public async Task<ResponseCreateUserDto> CreateAsync(RequestUserDto requestUserDto,
                                                            CancellationToken cancellationToken = default)
        {
            var user = requestUserDto.Adapt<User>();
            var identityResult = await _userManager.CreateAsync(user, requestUserDto.Password);

            if(!identityResult.Succeeded)
            {
                _logger.LogError("User creation failed. {Errors}", identityResult.Errors);

                throw new InvalidClientRequestException();
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, Role.User);

            if(!addToRoleResult.Succeeded)
            {
                _logger.LogError("Adding user to role failed. {Errors}", addToRoleResult.Errors);

                throw new Exception();
            }

            var response = user.Adapt<ResponseCreateUserDto>();

            _logger.LogInformation("User created successfully: {UserId}", user.Id);

            return response;
        }

        public async Task<KeysetPaginationResult<ResponseUserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var usersPaginationResult = await _paginationService.KeysetPaginateAsync(
                _userManager.Users,
                b => b.Descending(x => x.UserName!).Descending(x => x.Id),
                async id =>
                {
                    var guidId = Guid.Parse(id);
                    var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == guidId,
                                                                                cancellationToken);
                    NullCheckerUtilities.CheckUserExistence(user, logger, id);

                    return user;
                },
                query => query.Select(user => user.Adapt<ResponseUserDto>()));

            _logger.LogInformation("Pagination was successfully completed.");

            return usersPaginationResult;
        }

        public async Task<ResponseUserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            NullCheckerUtilities.CheckUserExistence(user, logger, id);

            var userDto = user.Adapt<ResponseUserDto>();

            _logger.LogInformation("Retrieved user by Id successfully. UserId: {UserId}", id);

            return userDto;
        }

        public async Task<ResponseUpdateUserDto> UpdateAsync(Guid id,
                                                            RequestUserDto user,
                                                            CancellationToken cancellationToken = default)
        {
            var existedUser = await _userManager.FindByIdAsync(id.ToString());

            NullCheckerUtilities.CheckUserExistence(existedUser, logger, id);

            User updatedUser = user.Adapt(existedUser)!;

            var updateResult = await _userManager.UpdateAsync(updatedUser);

            updateResult.CheckUserUpdateResult(_logger);

            var response = updatedUser.Adapt<ResponseUpdateUserDto>();

            _logger.LogInformation("Updated user successfully. UserId: {UserId}", id);

            return response;
        }
    }
}
