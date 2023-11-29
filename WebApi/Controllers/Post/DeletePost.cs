using BlogApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Post;

[ApiController]
[Tags("Post")]
[Route("posts/{Id:guid}")]
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
    [SwaggerOperation(Summary = "Delete Post")]
    public async Task<ActionResult> Delete([FromRoute] DeletePostCommand command, CancellationToken cancellationToken)
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