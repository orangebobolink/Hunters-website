using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Identity.Services.Extensions
{
    internal static class UserValidationExtensions
    {
        public static void CheckUserRefreshToken(this User user, string refreshToken, ILogger logger)
        {
            var isCorrect = user.RefreshToken != refreshToken;
            var isExpired = user.RefreshTokenExpiryTime <= DateTime.Now;

            if(isCorrect || isExpired)
            {
                logger.LogError("Refresh token is invalid.");

                throw new InvalidTokenException();
            }
        }

        public static void CheckIsPasswordCorrect(this bool isPasswordCorrect, ILogger logger, string username)
        {
            if(!isPasswordCorrect)
            {
                logger.LogError($"Incorrect password for user: {username}.");

                throw new InvalidPasswordException();
            }
        }
    }
}
