using BlogApi.Application.DTOs.Auth;
using BlogApi.Core.Interfaces.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Authentication;

[ApiController]
[Tags("Authentication")]
[Route("auth/login")]
[Produces("application/json")]
public class Login : ControllerBase
{
    private readonly ILogger<Login> _logger;
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;

    public Login(ILogger<Login> logger, IMediator mediator, IAuthService authService)
    {
        _logger = logger;
        _mediator = mediator;
        _authService = authService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Login")]
    public async Task<IActionResult> Post(LoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid payload");
            var (status, authenticatedResponse, message) = await _authService.Login(loginDto);
            if (status == 0)
                return BadRequest(message);
            return Ok(new TokenDto
            {
                AccessToken = authenticatedResponse.AccessToken,
                RefreshToken = authenticatedResponse.RefreshToken
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}