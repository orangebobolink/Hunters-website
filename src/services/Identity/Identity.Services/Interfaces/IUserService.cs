﻿using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;
using MR.AspNetCore.Pagination;

namespace Identity.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseUserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<KeysetPaginationResult<ResponseUserDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<ResponseCreateUserDto> CreateAsync(RequestUserDto user, CancellationToken cancellationToken);
        Task<ResponseUpdateUserDto> UpdateAsync(Guid id, RequestUserDto user, CancellationToken cancellationToken);
    }
}
