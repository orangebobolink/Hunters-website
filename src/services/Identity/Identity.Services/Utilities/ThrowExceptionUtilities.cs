using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Identity.Services.Utilities
{
    internal class ThrowExceptionUtilities<T>(ILogger<T> logger)
    {
        public User ThrowAccountNotFoundException(string username)
        {
            logger.LogError($"User not found during update. Username: {username}");

            throw new AccountNotFoundException(username);
        }

        public User ThrowAccountNotFoundException(Guid id)
        {
            logger.LogError($"User not found during update. UserId: {id}");

            throw new AccountNotFoundException(id);
        }
    }
}
