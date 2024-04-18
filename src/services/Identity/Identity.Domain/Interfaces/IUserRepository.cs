using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MR.EntityFrameworkCore.KeysetPagination;

namespace Identity.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByCredentialsAsync(
            string username,
            string email,
            string phoneNumber,
            CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByIdAsync(Guid id);
        Task<List<User>> GetKeysetPaginateAsync(
            Guid id,
            int numberTake,
            KeysetPaginationDirection keysetPaginationDirection,
            CancellationToken cancellationToken);
        Task<IdentityResult> CreateAsync(
            User entity,
            string password,
            CancellationToken cancellationToken);
        Task<IdentityResult> UpdateAsync(User entity, CancellationToken cancellationToken);
        Task<IdentityResult> DeleteAsync(User entity, CancellationToken cancellationToken);
    }
}
