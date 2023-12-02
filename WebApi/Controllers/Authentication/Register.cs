using BlogApi.Application.DTOs.Auth;
using BlogApi.Core.Interfaces.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Authentication;

[ApiController]
[Tags("Authentication")]
[Route("auth/register")]
[Produces("application/json")]
public class Register : ControllerBase
{
    private readonly ILogger<Register> _logger;
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;

    public Register(ILogger<Register> logger, IMediator mediator, IAuthService authService)
    {
        _logger = logger;
        _mediator = mediator;
        _authService = authService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Register")]
    public async Task<IActionResult> Post(RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid payload");
            var (status, message) = await _authService.Register(registerDto);
            if (status == 0)
            {
                return BadRequest(message);
            }

            return CreatedAtAction(nameof(Post), registerDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}