using BlogApi.Application.DTOs;
using BlogApi.Application.Queries;
using BlogApi.Shared.Helpers.Queryable;
using BlogApi.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.WebApi.Controllers.Tag;

[ApiController]
[Tags("Tag")]
[Route("tags")]
[Produces("application/json")]
public class GetTags : ControllerBase
{
    private readonly ILogger<GetTags> _logger;
    private readonly IMediator _mediator;

    public GetTags(ILogger<GetTags> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TagDto>>> Get([FromQuery] QueryParameters queryParams)
    {
        try
        {
            var tags = await _mediator.Send(new GetTagsQuery(queryParams));
            return Ok(tags);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching tags");

            return BadRequest("An error occurred while processing your request.");
        }
    }
}