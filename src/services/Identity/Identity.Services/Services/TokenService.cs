using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using Identity.Services.Dtos;
using Identity.Services.Dtos.ResponseDtos;
using Identity.Services.Interfaces;
using Identity.Services.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly JwtUtilities _jwtUtilities;
    private readonly ILogger<TokenService> _logger;

    public TokenService(UserManager<User> userManager, IConfiguration configuration,
        ILogger<TokenService> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _jwtUtilities = new JwtUtilities(userManager, configuration);
        _logger = logger;
    }

    public async Task<string> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];

        var claims = await _jwtUtilities.GetClaimsAsync(user);

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        int expiryTimeToken = _jwtUtilities.GetExpiryTimeToken();

        var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(expiryTimeToken),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        _logger.LogInformation("Access token was created successfully");

        return tokenString;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using(var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);

            _logger.LogInformation("Refresh token was created successfully");

            return Convert.ToBase64String(randomNumber);
        }
    }

    public async Task<ResponseAuthenticatedDto> Refresh(Guid id, TokenApiDto tokenApiModel,
                                                     CancellationToken cancellationToken = default)
    {
        if(tokenApiModel is null)
        {
            _logger.LogError("Token api is null");

            throw new InvalidTokenException();
        }

        string refreshToken = tokenApiModel.RefreshToken!;

        var user = await _userManager.FindByIdAsync(id.ToString());

        if(user is null)
        {
            _logger.LogError("User not found. UserId: {UserId}", id);

            throw new AccountNotFoundException(id);
        }

        if(user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            _logger.LogError("Refresh token is invalid.");

            throw new InvalidTokenException();
        }

        var newAccessToken = await GenerateAccessTokenAsync(user);
        var newRefreshToken = GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;

        var userUpdateResult = await _userManager.UpdateAsync(user);

        if(userUpdateResult.Succeeded == false)
        {
            _logger.LogError("Failed to update user during token refresh.");

            throw new UserUpdateException();
        }

        return new ResponseAuthenticatedDto()
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    public async Task Revoke(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if(user == null)
        {
            _logger.LogError("User not found. UserId: {UserId}", id);

            throw new AccountNotFoundException(id);
        }

        var userUpdateResult = await _userManager.UpdateAsync(user);

        if(userUpdateResult.Succeeded == false)
        {
            _logger.LogError("Failed to update user during token revocation.");

            throw new UserUpdateException();
        }
    }
}