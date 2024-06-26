﻿using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services.Utilities
{
    internal class AccessTokenUtility(UserManager<User> userManager, 
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration) 
        : IAccessTokenUtilities
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IConfiguration _configuration = configuration;

        public async Task<JwtSecurityToken> GetTokenOptionsAsync(User user, CancellationToken cancellationToken)
        {
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var claims = await GetClaimsAsync(user);
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

        private async Task<List<Claim>> GetClaimsAsync(User user)
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

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            var accessToken = token.Split(" ")[1];

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            var jwtTokenIsNull = jwtSecurityToken is null;
            var jwtTokenIsSecurityValid = !jwtSecurityToken!.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

            if(jwtTokenIsNull || jwtTokenIsSecurityValid)
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public string ReadAccessTokeFromHeaders()
        {
            string accessToken = _httpContextAccessor.HttpContext!.Request.Headers.Authorization!;

            if(string.IsNullOrEmpty(accessToken))
            {
                throw new Exception();
            }

            return accessToken;
        }

        public string GetUsernameFromAccessToken()
        {
            string accessToken = ReadAccessTokeFromHeaders();

            var principals = GetPrincipalFromExpiredToken(accessToken);

            var username = principals.Identity?.Name;

            return username!;
        }
    }
}
