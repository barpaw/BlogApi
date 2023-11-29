using BlogApi.Application.Commands;
using BlogApi.Application.DTOs;
using BlogApi.WebApi.Controllers.Comment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Category;

[ApiController]
[Tags("Category")]
[Route("categories/{Id:guid}")]
public class UpdateCategories : ControllerBase
{
    private readonly ILogger<UpdateCategories> _logger;
    private readonly IMediator _mediator;

    public UpdateCategories(ILogger<UpdateCategories> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Update Categories")]
    public async Task<ActionResult<TagDto>> Put([FromRoute] Guid Id, [FromBody] UpdateCategoryDto updateCommentDto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new UpdateCategoryCommand(Id, updateCommentDto);

        var result = await _mediator.Send(command, cancellationToken);

        if (result)
        {
            return Ok();
        }

        return NotFound();
    }
}