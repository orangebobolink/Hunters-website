using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Identity.Services.Dtos;
using Identity.Services.Dtos.RequestDtos;
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

        public TokenApiDto ThrowInvalidTokenException()
        {
            _logger.LogError("Token api is null");

            throw new InvalidTokenException();
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

        public RequestLoginUserDto ThrowInvalidClientRequestException()
        {
            _logger.LogError("Invalid client request: loginUserDto is null.");

            throw new InvalidClientRequestException();
        }
    }
}
