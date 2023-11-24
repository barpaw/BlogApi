using BlogApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Tag;

[ApiController]
[Tags("Tag")]
[Route("tags")]
[Produces("application/json")]
public class AddTag : ControllerBase
{
    private readonly ILogger<AddTag> _logger;
    private readonly IMediator _mediator;

    public AddTag(ILogger<AddTag> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(AddTagCommand command, CancellationToken cancellationToken)
    {
        var tag = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Post), new { id = tag.Id }, tag);
    }
}