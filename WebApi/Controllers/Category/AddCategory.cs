using BlogApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Category;

[ApiController]
[Tags("Category")]
[Route("categories")]
[Produces("application/json")]
public class AddCategory : ControllerBase
{
    private readonly ILogger<AddCategory> _logger;
    private readonly IMediator _mediator;

    public AddCategory(ILogger<AddCategory> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Add Category")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Post([FromBody] AddCategoryCommand command, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Post), new { id = category.Id }, category);
    }
}