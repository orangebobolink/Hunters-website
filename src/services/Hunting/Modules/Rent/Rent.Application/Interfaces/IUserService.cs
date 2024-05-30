using Rent.Application.Dtos.ResponseDtos;

namespace Rent.Application.Interfaces
{
    public interface IUserService
    {
        Task<RentUserResponseDto> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);
        Task<IEnumerable<RentUserResponseDto>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
