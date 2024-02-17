using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Identity.Services.Dtos.RequestDtos;
using Microsoft.Extensions.Logging;

namespace Identity.Services.Utilities
{
    internal static class NullCheckerUtilities
    {
        public static void CheckUserExistence(User? user, ILogger logger, Guid id)
        {
            var message = $"User not found during update. UserId: {id}";
            var exception = new AccountNotFoundException(id);

            CheckIsNull(user, logger, message, exception);
        }

        public static void CheckUserExistence(User? user, ILogger logger, string username)
        {
            var message = $"User not found during update. Username: {username}";
            var exception = new AccountNotFoundException(username);

            CheckIsNull(user, logger, message, exception);
        }

        public static void CheckRequestLoginUser(RequestLoginUserDto? loginUserDto, ILogger logger)
        {
            var message = "Invalid client request: loginUserDto is null.";
            var exception = new InvalidClientRequestException();

            CheckIsNull(loginUserDto, logger, message, exception);
        }

        private static void CheckIsNull<T>(T? obj, ILogger logger, string message, Exception exception)
        {
            if(obj == null)
            {
                logger.LogError(message);

                throw exception;
            }
        }
    }
}
