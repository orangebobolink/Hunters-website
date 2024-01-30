using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Identity.Services.Dtos;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public TokenService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<string> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];

        var claims = await GetClaimsAsync(user);

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        int expiryTimeToken = GetExpiryTimeToken();

        var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(expiryTimeToken),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return tokenString;
    }

    private async Task<List<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken = default)
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
        string expiryTimeTokenString = _configuration["JwtSettings:ExpiresInMinute"];

        if(!int.TryParse(expiryTimeTokenString, out int expiryTimeTokenInt))
        {
            throw new InvalidConfigurationException();
        }

        return expiryTimeTokenInt;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using(var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }

    public async Task<AuthenticatedResponse> Refresh(Guid id, TokenApiDto tokenApiModel, CancellationToken cancellationToken = default)
    {
        if(tokenApiModel is null)
            throw new InvalidTokenException();

        string refreshToken = tokenApiModel.RefreshToken!;

        var user = await _userManager.FindByIdAsync(id.ToString());

        if(user is null)
            throw new AccountNotFoundException(id);

        if(user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new InvalidTokenException();

        var newAccessToken = await GenerateAccessTokenAsync(user);
        var newRefreshToken = GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;

        var userUpdateResult = await _userManager.UpdateAsync(user);

        if(userUpdateResult.Succeeded == false)
            throw new UserUpdateException();

        return new AuthenticatedResponse()
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    public async Task Revoke(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if(user == null)
            throw new AccountNotFoundException(id);

        var userUpdateResult = await _userManager.UpdateAsync(user);

        if(userUpdateResult.Succeeded == false)
            throw new UserUpdateException();
    }
}