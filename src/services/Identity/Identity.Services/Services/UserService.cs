using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Extensions;
using Identity.Services.Interfaces;
using Identity.Services.Utilities;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using MR.EntityFrameworkCore.KeysetPagination;
using Shared.Helpers;
using Shared.Messages.UserMessages;

namespace Identity.Services.Services
{
    internal class UserService(
        IUserRepository userRepository,
        IBus bus,
        ILogger<UserService> logger)
        : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<UserService> _logger = logger;
        private readonly IPublishEndpoint _bus = bus;
        private readonly ThrowExceptionUtility<UserService> _throwExceptionUtilities = new(logger);

        public async Task<ResponseCreateUserDto> CreateAsync(
            RequestUserDto requestUserDto,
            CancellationToken cancellationToken)
        {
            var creditionals = requestUserDto.Adapt<User>();

            var existingUser = await _userRepository.GetByCredentialsAsync(
                creditionals,
                cancellationToken);

            if (existingUser is not null)
            {
                throw new Exception();
            }

            var user = requestUserDto.Adapt<User>();

            var userCreateResult = await _userRepository.CreateAsync(user, requestUserDto.Password);
            userCreateResult.CheckUserCreateResult(_logger);

            var addToRoleResult = await _userRepository.AddToRolesAsync(user, user.RoleNames);
            addToRoleResult.CheckAddToRoleResult(_logger);

            var message = user.Adapt<CreateUserMessage>();

            await _bus.Publish(message, cancellationToken);

            await _userRepository.SaveChangesAsync();

            var response = user.Adapt<ResponseCreateUserDto>();

            _logger.LogInformation("User created successfully: {UserId}", user.Id);

            return response;
        }

        public async Task<List<ResponseUserDto>> GetAllAsync(
            Guid id,
            int numberTake,
            KeysetPaginationDirection keysetPaginationDirection,
            CancellationToken cancellationToken)
        {
            var usersPaginationResult = await _userRepository.GetKeysetPaginateAsync(
                    id,
                    numberTake,
                    keysetPaginationDirection,
                    cancellationToken);

            foreach (var item in usersPaginationResult)
            {
                item.RoleNames = await _userRepository.GetRoles(item);
            }

            var respons = usersPaginationResult.Adapt<List<ResponseUserDto>>();

            _logger.LogInformation("Pagination was successfully completed.");

            return respons;
        }

        public async Task<List<ResponseUserDto>> GetALlByRoles(
            string roleName,
            CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllByRole(roleName);

            if (users is { Count: 0 })
            {
                ThrowHelper.ThrowKeyNotFoundException(roleName);
            }

            var response = users.Adapt<List<ResponseUserDto>>();

            return response;
        }

        public async Task<ResponseUserDto> GetUserByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var user = (await _userRepository.GetByIdAsync(id))
                ?? _throwExceptionUtilities.ThrowAccountNotFoundException(id);

            var userDto = user.Adapt<ResponseUserDto>();

            _logger.LogInformation("Retrieved user by Id successfully. UserId: {UserId}", id);

            return userDto;
        }

        public async Task<ResponseUpdateUserDto> UpdateAsync(
            Guid id,
            RequestUpdateUserDto user,
            CancellationToken cancellationToken)
        {
            var existedUser = (await _userRepository.GetByIdAsync(id))
                ?? _throwExceptionUtilities.ThrowAccountNotFoundException(id);

            User updatedUser = user.Adapt(existedUser)!;

            var updateResult = await _userRepository.UpdateAsync(updatedUser);

            updateResult.CheckUserUpdateResult(_logger);

            var oldRoles = await _userRepository.GetRoles(updatedUser);

            await UpdateUserRolesAsync(updatedUser, oldRoles);

            var message = user.Adapt<UpdateUserMessage>();

            await _bus.Publish(message, cancellationToken);

            await _userRepository.SaveChangesAsync();

            var response = updatedUser.Adapt<ResponseUpdateUserDto>();

            _logger.LogInformation("Updated user successfully. UserId: {UserId}", id);

            return response;
        }

        public async Task<ResponseUpdateUserDto> UpdatePasswordAsync(
            Guid id,
            UserChangePasswordRequestDto user,
            CancellationToken cancellationToken)
        {
            var existedUser = (await _userRepository.GetByIdAsync(id))
               ?? _throwExceptionUtilities.ThrowAccountNotFoundException(id);

            var result = await _userRepository.UpdatePasswordAsync(
                existedUser,
                user.CurrentPassword,
                user.NewPassword);

            result.CheckUserUpdateResult(logger);

            var message = user.Adapt<UpdateUserMessage>();

            await _bus.Publish(message, cancellationToken);

            await _userRepository.SaveChangesAsync();

            var response = existedUser.Adapt<ResponseUpdateUserDto>();

            _logger.LogInformation("Updated user successfully. UserId: {UserId}", id);

            return response;
        }

        private async Task UpdateUserRolesAsync(User user, List<string> oldRoles)
        {
            var newRoles = user.RoleNames
               .Except(oldRoles)
               .ToList();
            var removedRoles = oldRoles
                .Except(user.RoleNames)
                .ToList();

            if (newRoles.Count != 0)
            {
                var addToRoleResult = await _userRepository
                    .AddToRolesAsync(user, [.. newRoles]);

                addToRoleResult.CheckAddToRoleResult(_logger);
            }

            if (removedRoles.Count != 0)
            {
                var removeFromRoleResult = await _userRepository
                    .RemoveFromRolesAsync(user, [.. removedRoles]);

                removeFromRoleResult.CheckAddToRoleResult(_logger);
            }
        }
    }
}