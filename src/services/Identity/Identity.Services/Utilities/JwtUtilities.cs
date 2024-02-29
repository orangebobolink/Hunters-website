﻿using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services.Utilities
{
    internal class JwtUtilities(UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration)
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;
        private readonly IConfiguration _configuration = configuration;
        private readonly string _cookieRefreshTokenKey = configuration["JwtSettings:RefreshToken:CookieName"]!;
        public void AddRefreshTokenCookie(string newRefreshToken)
        {
            var expiresInDay = int.Parse(_configuration["JwtSettings:RefreshToken:ExpiresInDay"]!);

            var refreshTokenCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(expiresInDay),
                Path = "/",
            };

            _httpContext.Response
                .Cookies.Append(_cookieRefreshTokenKey, newRefreshToken, refreshTokenCookieOptions);
        }

        public string ReadRefreshTokenCookie()
        {
            var data = _httpContext.Request
                                .Cookies[_cookieRefreshTokenKey]!;

            return data;
        }

        public void DeleteRefreshTokenCookie()
        {
            _httpContext.Response
                    .Cookies.Delete(_cookieRefreshTokenKey);
        }

        public async Task<JwtSecurityToken> GetTokenOptionsAsync(User user, CancellationToken cancellationToken)
        {
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var claims = await GetClaimsAsync(user, cancellationToken);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            int expiryTimeToken = GetExpiryTimeToken();

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryTimeToken),
                signingCredentials: signinCredentials
            );

            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claims.AddRange(roleClaims);

            return claims;
        }

        private int GetExpiryTimeToken()
        {
            string expiryTimeTokenString = _configuration["JwtSettings:ExpiresInMinute"]!;

            if(!int.TryParse(expiryTimeTokenString, out int expiryTimeTokenInt))
            {
                throw new InvalidConfigurationException();
            }

            return expiryTimeTokenInt;
        }
    }
}
