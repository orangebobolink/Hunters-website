using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Identity.Services.Utilities
{
    internal class JwtUtilities
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public JwtUtilities(UserManager<User> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<List<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken = default)
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

        public int GetExpiryTimeToken()
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
