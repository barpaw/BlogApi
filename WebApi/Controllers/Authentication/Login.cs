using BlogApi.Application.Commands;
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

    public Login(ILogger<Login> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Login")]
    public async Task<ActionResult> Post(LoginCommand loginCommand, CancellationToken cancellationToken)
    {
        var (status, authenticatedResponse, message) = await _mediator.Send(loginCommand, cancellationToken);
        if (status == 0)
            return BadRequest(message);
        return Ok(new TokenDto
        {
            AccessToken = authenticatedResponse.AccessToken,
            RefreshToken = authenticatedResponse.RefreshToken
        });


        //     _logger.LogError(ex.Message);
        //     return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
}