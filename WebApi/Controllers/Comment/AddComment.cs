using BlogApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Comment;

[ApiController]
[Tags("Comment")]
[Route("comments")]
[Produces("application/json")]
public class AddComment : ControllerBase
{
    private readonly ILogger<AddComment> _logger;
    private readonly IMediator _mediator;

    public AddComment(ILogger<AddComment> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Add Comment")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult> Post([FromBody] AddCommentCommand command, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var comment = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Post), new { id = comment.Id }, comment);
    }
}