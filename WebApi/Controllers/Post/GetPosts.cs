using BlogApi.Application.DTOs;
using BlogApi.Application.Queries;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Post;

[ApiController]
[Tags("Post")]
[Route("posts")]
[Produces("application/json")]
public class GetPosts : ControllerBase
{
    private readonly ILogger<GetPosts> _logger;
    private readonly IMediator _mediator;

    public GetPosts(ILogger<GetPosts> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Get Posts")]
    public async Task<ActionResult<IEnumerable<PostDto>>> Get([FromQuery] QueryParameters queryParams)
    {
        try
        {
            var tags = await _mediator.Send(new GetPostsQuery(queryParams));
            return Ok(tags);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching posts");

            return BadRequest("An error occurred while processing your request.");
        }
    }
}