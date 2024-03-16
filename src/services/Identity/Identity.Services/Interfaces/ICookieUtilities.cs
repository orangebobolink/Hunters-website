using Microsoft.AspNetCore.Http;

namespace Identity.Services.Interfaces
{
    public interface ICookieUtilities
    {
        void AddToCookie(string key, string value, CookieOptions cookieOptions);
        void RemoveFromCookie(string key);
        string? ReadFromCookie(string key);
    }
}
