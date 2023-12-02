using BlogApi.Application.DTOs.Auth;
using BlogApi.Core.Interfaces.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Token;

[ApiController]
[Tags("Token")]
[Route("token/refresh")]
[Produces("application/json")]
public class RefreshToken : ControllerBase
{
    private readonly ILogger<RefreshToken> _logger;
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;

    public RefreshToken(ILogger<RefreshToken> logger, IMediator mediator, ITokenService tokenService)
    {
        _logger = logger;
        _mediator = mediator;
        _tokenService = tokenService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Refresh token")]
    public async Task<IActionResult> Post(TokenDto tokenDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid payload");
            var (status, authenticatedResponse, message) = await _tokenService.RefreshToken(tokenDto);
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