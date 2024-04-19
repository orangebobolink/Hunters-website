using Identity.Domain.Entities;

namespace Identity.Services.Interfaces
{
    public interface IRefreshTokenUtilities
    {
        void ChangeRefreshTokenForUser(User user, string newRefreshToken);
    }
}
