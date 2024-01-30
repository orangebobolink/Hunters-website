using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services.Services
{
    internal class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseCreateUserDto> CreateAsync(RequestUserDto requestUserDto, CancellationToken cancellationToken = default)
        {
            var user = requestUserDto.Adapt<User>();
            var identityResult = await _userManager.CreateAsync(user, requestUserDto.Password);

            if(!identityResult.Succeeded)
            {
                throw new InvalidClientRequestException();
            }

            var result = await _userManager.AddToRoleAsync(user, Role.User);

            if(!result.Succeeded)
            {
                throw new Exception();
            }

            var response = user.Adapt<ResponseCreateUserDto>();

            return response;
        }

        public async Task<List<ResponseUserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userManager.Users.ToListAsync();

            if(users == null || users.Count == 0)
            {


                return new List<ResponseUserDto>();
            }

            var usersDto = users.Adapt<List<ResponseUserDto>>();

            return usersDto;
        }

        public async Task<ResponseUserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null)
            {


                throw new AccountNotFoundException(id);
            }

            var userDto = user.Adapt<ResponseUserDto>();

            return userDto;
        }

        public async Task<ResponseUpdateUserDto> UpdateAsync(Guid id,
                                                            RequestUserDto user,
                                                            CancellationToken cancellationToken = default)
        {
            var existedUser = await _userManager.FindByIdAsync(id.ToString());

            if(existedUser == null)
            {


                throw new AccountNotFoundException(id);
            }

            var newUser = user.Adapt(existedUser);

            var result = await _userManager.UpdateAsync(newUser);

            if(!result.Succeeded)
            {
                throw new UserUpdateException();
            }

            var response = newUser.Adapt<ResponseUpdateUserDto>();

            return response;
        }
    }
}
