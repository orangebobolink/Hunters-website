using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MR.EntityFrameworkCore.KeysetPagination;

namespace Identity.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByCredentialsAsync(
            User creditionals,
            CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByIdAsync(Guid id);
        Task<List<string>> GetRoles(User user);
        Task<List<User>> GetKeysetPaginateAsync(
            Guid id,
            int numberTake,
            KeysetPaginationDirection keysetPaginationDirection,
            CancellationToken cancellationToken);
        Task<IdentityResult> CreateAsync(
            User entity,
            string password);
        Task<IdentityResult> AddToRolesAsync(User user, List<string> role);
        Task<IdentityResult> RemoveFromRolesAsync(User user, List<string> role);
        Task<IdentityResult> UpdateAsync(User entity);
        Task<IdentityResult> DeleteAsync(User entity);
    }
}
