using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Identity.Services.Utilities
{
    internal class CookieUtilities(IHttpContextAccessor httpContextAccessor)
        : ICookieUtilities
    {
        private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

        public void AddToCookie(string key, string value, CookieOptions cookieOptions)
        {
            _httpContext.Response.Cookies.Append(key, value, cookieOptions);
        }

        public string? ReadFromCookie(string key)
        {
            var data = _httpContext.Request.Cookies[key];

            return data;
        }

        public void RemoveFromCookie(string key)
        {
            _httpContext.Response.Cookies.Delete(key);
        }
    }
}
