using BlogApi.Application.Commands;
using BlogApi.Application.DTOs.Comment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Comment;

[ApiController]
[Tags("Comment")]
[Route("comments/{Id:guid}")]
public class UpdateComment : ControllerBase
{
    private readonly ILogger<UpdateComment> _logger;
    private readonly IMediator _mediator;

    public UpdateComment(ILogger<UpdateComment> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Update Comment")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<CommentDto>> Put([FromRoute] Guid Id, [FromBody] UpdateCommentDto updateCommentDto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new UpdateCommentCommand(Id, updateCommentDto);

        var result = await _mediator.Send(command, cancellationToken);

        if (result)
        {
            return Ok();
        }

        return NotFound();
    }
}