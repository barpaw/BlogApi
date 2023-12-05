using BlogApi.Application.Commands;
using BlogApi.WebApi.Controllers.Comment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Category;

[ApiController]
[Tags("Category")]
[Route("categories/{Id:guid}")]
public class DeleteCategory : ControllerBase
{
    private readonly ILogger<DeleteCategory> _logger;
    private readonly IMediator _mediator;

    public DeleteCategory(ILogger<DeleteCategory> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Delete Category")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete([FromRoute] DeleteCategoryCommand command, CancellationToken cancellationToken)
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