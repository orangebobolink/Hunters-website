namespace Identity.Services.Interfaces
{
    public interface IRefreshTokenCookie
    {
        void AddRefreshTokenCookie(string newRefreshToken);
        string ReadRefreshTokenCookie();
        void DeleteRefreshTokenCookie();
    }
}
