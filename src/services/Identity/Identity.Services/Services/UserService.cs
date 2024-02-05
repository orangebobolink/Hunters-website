using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MR.AspNetCore.Pagination;

namespace Identity.Services.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IPaginationService _paginationService;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<User> userManager, IPaginationService paginationService,
            ILogger<UserService> logger)
        {
            _userManager = userManager;
            _paginationService = paginationService;
            _logger = logger;
        }

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

            var result = await _userManager.AddToRoleAsync(user, Role.User);

            if(!result.Succeeded)
            {
                _logger.LogError("Adding user to role failed. {Errors}", result.Errors);

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
                    var reference = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == guidId,
                                                                                cancellationToken);
                    if(reference == null)
                    {
                        _logger.LogError("User not found during pagination. UserId: {UserId}", guidId);

                        throw new AccountNotFoundException(guidId);
                    }

                    return reference;
                },
                query => query.Select(user => user.Adapt<ResponseUserDto>()));

            _logger.LogInformation("Pagination was successfully completed.");

            return usersPaginationResult;
        }

        public async Task<ResponseUserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null)
            {
                _logger.LogError("User not found. UserId: {UserId}", id);

                throw new AccountNotFoundException(id);
            }

            var userDto = user.Adapt<ResponseUserDto>();

            _logger.LogInformation("Retrieved user by Id successfully. UserId: {UserId}", id);

            return userDto;
        }

        public async Task<ResponseUpdateUserDto> UpdateAsync(Guid id,
                                                            RequestUserDto user,
                                                            CancellationToken cancellationToken = default)
        {
            var existedUser = await _userManager.FindByIdAsync(id.ToString());

            if(existedUser == null)
            {
                _logger.LogError("User not found during update. UserId: {UserId}", id);

                throw new AccountNotFoundException(id);
            }

            var newUser = user.Adapt(existedUser);

            var result = await _userManager.UpdateAsync(newUser);

            if(!result.Succeeded)
            {
                _logger.LogError("User update failed. UserId: {UserId}", id);

                throw new UserUpdateException();
            }

            var response = newUser.Adapt<ResponseUpdateUserDto>();

            _logger.LogInformation("Updated user successfully. UserId: {UserId}", id);

            return response;
        }
    }
}
