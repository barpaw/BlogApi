using BlogApi.Application.DTOs;
using BlogApi.Application.Queries;
using BlogApi.WebApi.Controllers.Comment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Category;

[ApiController]
[Tags("Category")]
[Route("category/{Id:guid}")]
[Produces("application/json")]
public class GetCategory : ControllerBase
{
    private readonly ILogger<GetCategory> _logger;
    private readonly IMediator _mediator;

    public GetCategory(ILogger<GetCategory> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Get Category By Id")]
    public async Task<ActionResult<CategoryDto>> Get([FromRoute] Guid Id)
    {
        try
        {
            var category = await _mediator.Send(new GetCategoryQuery(Id));

            if (category is null)
            {
                return NotFound();
            }

            return Ok(category);
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching category");

            return BadRequest("An error occurred while processing your request.");
        }
    }
}