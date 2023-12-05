using BlogApi.Application.DTOs.Auth;
using BlogApi.Core.Interfaces.Auth;
using MediatR;

namespace BlogApi.Application.Commands.Handlers;

public sealed class LoginHandler : IRequestHandler<LoginCommand, (int, TokenDto?, string)>
{
    private readonly ILogger<LoginHandler> _logger;
    private readonly IAuthService _authService;

    public LoginHandler(ILogger<LoginHandler> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    public async Task<(int, TokenDto?, string)> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        return await _authService.Login(command);
    }
}