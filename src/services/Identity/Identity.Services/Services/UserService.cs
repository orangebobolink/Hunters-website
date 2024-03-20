using Identity.Domain.Entities;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Extensions;
using Identity.Services.Interfaces;
using Identity.Services.Utilities;
using Mapster;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MR.AspNetCore.Pagination;
using Shared.Messages.UserMessages;

namespace Identity.Services.Services
{
    internal class UserService(UserManager<User> userManager,
        IPaginationService paginationService,
        IPublishEndpoint publishEndpoint,
        ILogger<UserService> logger) : IUserService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IPaginationService _paginationService = paginationService;
        private readonly ILogger<UserService> _logger = logger;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly ThrowExceptionUtilities<UserService> _throwExceptionUtilities = new(logger);

        public async Task<ResponseCreateUserDto> CreateAsync(RequestUserDto requestUserDto,
                                                            CancellationToken cancellationToken)
        {
            var user = requestUserDto.Adapt<User>();

            var userCreateResult = await _userManager.CreateAsync(user, requestUserDto.Password);
            userCreateResult.CheckUserCreateResult(_logger);

            var addToRoleResult = await _userManager.AddToRoleAsync(user, Role.User);
            addToRoleResult.CheckAddToRoleResult(_logger);

            var message = user.Adapt<CreateUserMessage>();

            await _publishEndpoint.Publish(message);

            var response = user.Adapt<ResponseCreateUserDto>();

            _logger.LogInformation("User created successfully: {UserId}", user.Id);

            return response;
        }

        public async Task<KeysetPaginationResult<ResponseUserDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var usersPaginationResult = await GetKeysetPaginateAsync(cancellationToken);

            _logger.LogInformation("Pagination was successfully completed.");

            return usersPaginationResult;
        }

        public async Task<ResponseUserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = (await _userManager.FindByIdAsync(id.ToString()))
                ?? _throwExceptionUtilities.ThrowAccountNotFoundException(id);

            var userDto = user.Adapt<ResponseUserDto>();

            _logger.LogInformation("Retrieved user by Id successfully. UserId: {UserId}", id);

            return userDto;
        }

        public async Task<ResponseUpdateUserDto> UpdateAsync(Guid id,
                                                            RequestUserDto user,
                                                            CancellationToken cancellationToken)
        {
            var existedUser = (await _userManager.FindByIdAsync(id.ToString()))
                ?? _throwExceptionUtilities.ThrowAccountNotFoundException(id);

            User updatedUser = user.Adapt(existedUser)!;

            var updateResult = await _userManager.UpdateAsync(updatedUser);
            updateResult.CheckUserUpdateResult(_logger);

            var message = user.Adapt<UpdateUserMessage>();

            await _publishEndpoint.Publish(message);

            var response = updatedUser.Adapt<ResponseUpdateUserDto>();

            _logger.LogInformation("Updated user successfully. UserId: {UserId}", id);

            return response;
        }

        private async Task<KeysetPaginationResult<ResponseUserDto>> GetKeysetPaginateAsync(CancellationToken cancellationToken)
        {
            var usersPaginationResult = await _paginationService.KeysetPaginateAsync(
               _userManager.Users,
               b => b.Descending(x => x.UserName!).Descending(x => x.Id),
               async id => await GetReferenceAsync(id, cancellationToken),
               query => query.Select(user => user.Adapt<ResponseUserDto>()));

            return usersPaginationResult;
        }

        private async Task<User> GetReferenceAsync(string id, CancellationToken cancellationToken)
        {
            var guidId = Guid.Parse(id);
            var user = (await _userManager.Users.FirstOrDefaultAsync(user => user.Id == guidId,
                                                                        cancellationToken))
                                    ?? _throwExceptionUtilities.ThrowAccountNotFoundException(id);

            return user!;
        }
    }
}
