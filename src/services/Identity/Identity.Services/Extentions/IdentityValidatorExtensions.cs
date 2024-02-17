using Identity.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Services.Extentions
{
    static internal class IdentityValidatorExtensions
    {
        public static void CheckUserUpdateResult(this IdentityResult userUpdateResult, ILogger logger)
        {
            if(!userUpdateResult.Succeeded)
            {
                logger.LogError("Failed to update user.");

                throw new UserUpdateException();
            }
        }
    }
}
