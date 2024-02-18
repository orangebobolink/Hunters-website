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

        public static void CheckUserCreateResult(this IdentityResult userCreateResult, ILogger logger)
        {
            if(!userCreateResult.Succeeded)
            {
                logger.LogError("User creation failed. {Errors}", userCreateResult.Errors);

                throw new InvalidClientRequestException();
            }
        }

        public static void CheckAddToRoleResult(this IdentityResult addToRoleResult, ILogger logger)
        {
            if(!addToRoleResult.Succeeded)
            {
                logger.LogError("Adding user to role failed. {Errors}", addToRoleResult.Errors);

                throw new Exception();
            }
        }
    }
}
