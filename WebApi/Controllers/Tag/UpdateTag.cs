using BlogApi.Application.Commands;
using BlogApi.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.WebApi.Controllers.Tag;

[ApiController]
[Tags("Tag")]
[Route("tags/{Id:guid}")]
public class UpdateTag : ControllerBase
{
    private readonly ILogger<UpdateTag> _logger;
    private readonly IMediator _mediator;

    public UpdateTag(ILogger<UpdateTag> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TagDto>> Put([FromRoute] Guid Id, [FromBody] TagWithoutIdDto tagWithoutIdDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new UpdateTagCommand(new TagDto(Id, tagWithoutIdDto.Name));

        var result = await _mediator.Send(command, cancellationToken);

        if (result)
        {
            return Ok();
        }

        return NotFound();
    }
}