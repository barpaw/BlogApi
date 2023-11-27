using BlogApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.WebApi.Controllers.Tag;

[ApiController]
[Tags("Tag")]
[Route("tags/{Id:guid}")]
public class DeleteTag : ControllerBase
{
    private readonly ILogger<DeleteTag> _logger;
    private readonly IMediator _mediator;

    public DeleteTag(ILogger<DeleteTag> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete([FromRoute] DeleteTagCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (result)
        {
            return NoContent();
        }

        return NotFound();
    }
}