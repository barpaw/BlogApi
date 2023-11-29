using BlogApi.Application.DTOs;
using BlogApi.Application.Queries;
using BlogApi.WebApi.Controllers.Tag;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Post;

[ApiController]
[Tags("Post")]
[Route("post/{Id:guid}")]
[Produces("application/json")]
public class GetPost : ControllerBase
{
    private readonly ILogger<GetPost> _logger;
    private readonly IMediator _mediator;

    public GetPost(ILogger<GetPost> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Get Post By Id")]
    public async Task<ActionResult<PostDto>> Get([FromRoute] Guid Id)
    {
        try
        {
            var tag = await _mediator.Send(new GetPostQuery(Id));
            return Ok(tag);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching tag");

            return BadRequest("An error occurred while processing your request.");
        }
    }
}