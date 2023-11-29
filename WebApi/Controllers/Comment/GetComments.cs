using BlogApi.Application.DTOs;
using BlogApi.Application.Queries;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Comment;

[ApiController]
[Tags("Comment")]
[Route("comments")]
[Produces("application/json")]
public class GetComments : ControllerBase
{
    private readonly ILogger<GetComments> _logger;
    private readonly IMediator _mediator;

    public GetComments(ILogger<GetComments> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Get Comments")]
    public async Task<ActionResult<IEnumerable<PostDto>>> Get([FromQuery] QueryParameters queryParams)
    {
        try
        {
            var tags = await _mediator.Send(new GetCommentsQuery(queryParams));
            return Ok(tags);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching comments");

            return BadRequest("An error occurred while processing your request.");
        }
    }
}