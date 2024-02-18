using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Identity.Services.Utilities
{
    internal class ThrowExceptionUtilities<T>
    {
        private readonly ILogger<T> _logger;

        public ThrowExceptionUtilities(ILogger<T> logger)
        {
            _logger = logger;
        }

        public User ThrowAccountNotFoundException(string username)
        {
            _logger.LogError($"User not found during update. Username: {username}");

            throw new AccountNotFoundException(username);
        }

        public User ThrowAccountNotFoundException(Guid id)
        {
            _logger.LogError($"User not found during update. UserId: {id}");

            throw new AccountNotFoundException(id);
        }
    }
}
