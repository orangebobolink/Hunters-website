using Identity.Domain.Entities;

namespace Identity.Services.Interfaces
{
    public interface IRefreshTokenUtilities
    {
        void UpdateRefreshTokenForUser(User user, string newRefreshToken);
    }
}
