using System.Security.Claims;
using BlogApi.Application.DTOs.Auth;

namespace BlogApi.Core.Interfaces.Auth;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    Task<(int, TokenDto?, string)> RefreshToken(TokenDto? tokenDto);
    Task<(int, string)> RevokeToken(string username);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string? token);
}