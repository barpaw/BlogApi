using BlogApi.Core.Interfaces.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Token;

[ApiController]
[Tags("Token")]
[Route("token/revoke/{username}")]
[Produces("application/json")]
[Authorize]
public class RevokeToken : ControllerBase
{
    private readonly ILogger<RevokeToken> _logger;
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;

    public RevokeToken(ILogger<RevokeToken> logger, IMediator mediator, ITokenService tokenService)
    {
        _logger = logger;
        _mediator = mediator;
        _tokenService = tokenService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Revoke user token")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> Post(string username)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid payload");
            
            var (status, message) = await _tokenService.RevokeToken(username);
            if (status == 0)
                return BadRequest(message);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}