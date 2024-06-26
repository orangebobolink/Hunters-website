﻿using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using MR.AspNetCore.Pagination;
using MR.EntityFrameworkCore.KeysetPagination;

namespace Identity.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseUserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<ResponseUserDto>> GetAllAsync(
            Guid id,
            int numberTake,
            KeysetPaginationDirection keysetPaginationDirection,
            CancellationToken cancellationToken);
        Task<ResponseCreateUserDto> CreateAsync(RequestUserDto user, CancellationToken cancellationToken);
        Task<ResponseUpdateUserDto> UpdateAsync(Guid id, RequestUpdateUserDto user, CancellationToken cancellationToken);
        Task<ResponseUpdateUserDto> UpdatePasswordAsync(Guid id, UserChangePasswordRequestDto user, CancellationToken cancellationToken);
        Task<List<ResponseUserDto>> GetALlByRoles(string roleName, CancellationToken cancellationToken);
    }
}
