using BlogApi.Application.Commands;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Post;
using BlogApi.Application.DTOs.Tag;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Post;

[ApiController]
[Tags("Post")]
[Route("posts/{Id:guid}")]
public class UpdatePost : ControllerBase
{
    private readonly ILogger<UpdatePost> _logger;
    private readonly IMediator _mediator;

    public UpdatePost(ILogger<UpdatePost> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Update Post")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TagDto>> Put([FromRoute] Guid Id, [FromBody] UpdatePostDto updatePostDto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new UpdatePostCommand(Id, updatePostDto);

        var result = await _mediator.Send(command, cancellationToken);

        if (result)
        {
            return Ok();
        }

        return NotFound();
    }
}