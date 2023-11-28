using BlogApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.WebApi.Controllers.Post;

[ApiController]
[Tags("Post")]
[Route("posts")]
[Produces("application/json")]
public class AddPost : ControllerBase
{
    private readonly ILogger<AddPost> _logger;
    private readonly IMediator _mediator;

    public AddPost(ILogger<AddPost> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] AddPostCommand command, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var post = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Post), new { id = post.Id }, post);
    }
}