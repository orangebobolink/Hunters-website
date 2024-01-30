using Identity.Domain.Entities;
using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;

namespace Identity.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseUserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<ResponseUserDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ResponseCreateUserDto> CreateAsync(RequestUserDto user, CancellationToken cancellationToken = default);
        Task<ResponseUpdateUserDto> UpdateAsync(Guid id, RequestUserDto user, CancellationToken cancellationToken = default);
    }
}
