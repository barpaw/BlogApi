using BlogApi.Application.DTOs;
using BlogApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Tag;

[ApiController]
[Tags("Tag")]
[Route("tag/{Id:guid}")]
[Produces("application/json")]
public class GetTag : ControllerBase
{
    private readonly ILogger<GetTag> _logger;
    private readonly IMediator _mediator;

    public GetTag(ILogger<GetTag> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Get Tag By Id")]
    public async Task<ActionResult<TagDto>> Get([FromRoute] Guid Id)
    {
        try
        {
            var tag = await _mediator.Send(new GetTagQuery(Id));

            if (tag is null)
            {
                return NotFound();
            }

            return Ok(tag);
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching tag");

            return BadRequest("An error occurred while processing your request.");
        }
    }
}