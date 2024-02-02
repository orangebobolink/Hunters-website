using Identity.Services.Dtos.RequestDtos;
using Identity.Services.Dtos.ResponseDtos;

namespace Identity.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseUserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<ResponseUserDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<ResponseCreateUserDto> CreateAsync(RequestUserDto user, CancellationToken cancellationToken);
        Task<ResponseUpdateUserDto> UpdateAsync(Guid id, RequestUserDto user, CancellationToken cancellationToken);
    }
}
