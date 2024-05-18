using Mapster;
using Rent.Application.Dtos.ResponseDtos;
using Rent.Application.Interfaces;
using Rent.Domain.Interfaces;

namespace Rent.Application.Services
{
    internal class UserService(IUserRepository userRepository)
                : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<IEnumerable<RentUserResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository
                .GetAllAsync(cancellationToken, true);

            var response = users.Adapt<IEnumerable<RentUserResponseDto>>();

            return response;
        }

        public async Task<RentUserResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository
               .GetByPredicate(
               p => p.Id == id,
               cancellationToken,
               true);

            var response = user.Adapt<RentUserResponseDto>();

            return response;
        }
    }
}
