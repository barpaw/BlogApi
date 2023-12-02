using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlogApi.Application.DTOs.Auth;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.Auth;
using BlogApi.Core.Interfaces.UoW;
using Microsoft.AspNetCore.Identity;

namespace BlogApi.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<(int, string)> Register(RegisterDto registerDto)
    {
        var userExists = await _userManager.FindByNameAsync(registerDto.Username);
        if (userExists != null)
            return (0, "User already exists");

        User user = new()
        {
            Email = registerDto.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerDto.Username,
        };
        var createUserResult = await _userManager.CreateAsync(user, registerDto.Password);
        if (!createUserResult.Succeeded)
            return (0, "User creation failed! Please check user details and try again.");

        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }

        await _userManager.AddToRoleAsync(user, UserRoles.User);

        return (1, "User created successfully!");
    }

    public async Task<(int, string)> RegisterAdmin(RegisterDto registerDto)
    {
        
        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            return (0, "Admin already exists.");
        }
        
        var userExists = await _userManager.FindByNameAsync(registerDto.Username);
        if (userExists != null)
            return (0, "Username is used!");

        User user = new()
        {
            Email = registerDto.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerDto.Username
        };
        
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
            return (0, "User creation failed! Please check user details and try again.");

        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        await _userManager.AddToRoleAsync(user, UserRoles.Admin);

        return (1, "Admin created successfully!");
    }

    public async Task<(int, TokenDto?, string)> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null)
            return (0, null, "Invalid username");
        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return (0, null, "Invalid password");

        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var accessToken = _tokenService.GenerateAccessToken(authClaims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        _ = int.TryParse(_configuration["JWTKey:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTimeOffset.Now.AddDays(refreshTokenValidityInDays);

        await _userManager.UpdateAsync(user);

        var authenticatedResponse = new TokenDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return (1, authenticatedResponse, "Ok");
    }
}