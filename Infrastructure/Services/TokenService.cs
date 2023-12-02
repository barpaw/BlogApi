using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BlogApi.Application.DTOs.Auth;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public TokenService(IConfiguration configuration, UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        _ = int.TryParse(_configuration["JWTKey:TokenValidityInMinutes"], out int tokenValidityInMinutes);

        var tokeOptions = new JwtSecurityToken(
            issuer: _configuration["JWTKey:ValidIssuer"],
            audience: _configuration["JWTKey:ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public async Task<(int, TokenDto?, string)> RefreshToken(TokenDto? tokenDto)
    {
        if (tokenDto is null)
            return (0, null, "Invalid client request");
        string? accessToken = tokenDto.AccessToken;
        string? refreshToken = tokenDto.RefreshToken;
        var principal = GetPrincipalFromExpiredToken(accessToken);
        var username = principal.Identity.Name; //this is mapped to the Name claim by default

        var user = await _userManager.FindByNameAsync(username);
        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTimeOffset.Now)
            return (0, null, "Invalid client request");

        var newAccessToken = GenerateAccessToken(principal.Claims);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await _userManager.UpdateAsync(user);

        var newTokenDto = new TokenDto()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };

        return (1, newTokenDto, "Ok");
    }

    public async Task<(int, string)> RevokeToken(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null)
            return (0, "Invalid client request. User doesn't exists.");

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;

        await _userManager.UpdateAsync(user);

        return (1, "Ok.");
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"])),
            ValidateLifetime = true //here we are saying that we don't care about the token's expiration date
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}