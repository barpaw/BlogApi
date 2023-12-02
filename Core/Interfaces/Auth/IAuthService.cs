using System.Security.Claims;
using BlogApi.Application.DTOs.Auth;

namespace BlogApi.Core.Interfaces.Auth;

public interface IAuthService
{
    Task<(int, string)> Register(RegisterDto registerDto);

    Task<(int, string)> RegisterAdmin(RegisterDto registerDto);
    Task<(int, TokenDto?, string)> Login(LoginDto loginDto);
}