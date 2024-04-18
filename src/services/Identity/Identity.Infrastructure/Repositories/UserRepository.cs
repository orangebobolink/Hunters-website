using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MR.EntityFrameworkCore.KeysetPagination;

namespace Identity.Infrastructure.Repositories
{
    internal class UserRepository(
        UserManager<User> userManager)
        : IUserRepository
    {
        private readonly UserManager<User> _userManager = userManager;

        public async Task<IdentityResult> CreateAsync(
            User entity,
            string password)
        {
            return await _userManager.CreateAsync(entity, password);
        }

        public async Task<IdentityResult> DeleteAsync(User entity)
        {
            return await _userManager.DeleteAsync(entity);
        }

        public async Task<List<User>> GetKeysetPaginateAsync(
            Guid id,
            int numberTake,
            KeysetPaginationDirection keysetPaginationDirection,
            CancellationToken cancellationToken)
        {
            var reference = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            var keysetContext = _userManager.Users.KeysetPaginate(
                b => b.Descending(x => x.UserName!).Descending(x => x.Id),
                keysetPaginationDirection,
                reference);

            return await keysetContext.Query
                .Take(numberTake)
                .ToListAsync();
        }

        public async Task<User?> GetByCredentialsAsync(
            User creditionals,
            CancellationToken cancellationToken)
        {
            return await _userManager.Users
                    .FirstOrDefaultAsync(u =>
                        u.Email == creditionals.Email
                        || u.UserName == creditionals.UserName
                        || u.PhoneNumber == creditionals.PhoneNumber,
                        cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<IdentityResult> UpdateAsync(User entity)
        {
            return await _userManager.DeleteAsync(entity);
        }

        public async Task<IdentityResult> AddToRoleAsync(
            User user,
            string role = Role.User)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }
    }
}
