using BlogApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Comment;

[ApiController]
[Tags("Comment")]
[Route("comments/{Id:guid}")]
public class DeleteComment : ControllerBase
{
    private readonly ILogger<DeleteComment> _logger;
    private readonly IMediator _mediator;

    public DeleteComment(ILogger<DeleteComment> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Delete Comment")]
    public async Task<ActionResult> Delete([FromRoute] DeleteCommentCommand command, CancellationToken cancellationToken)
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