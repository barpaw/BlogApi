using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Category;
using BlogApi.Application.Queries;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Category;

[ApiController]
[Tags("Category")]
[Route("categories")]
[Produces("application/json")]
public class GetCategories : ControllerBase
{
    private readonly ILogger<GetCategories> _logger;
    private readonly IMediator _mediator;

    public GetCategories(ILogger<GetCategories> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Get Categories")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get([FromQuery] QueryParameters queryParams)
    {
        try
        {
            var categories = await _mediator.Send(new GetCategoriesQuery(queryParams));
            return Ok(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching categories");

            return BadRequest("An error occurred while processing your request.");
        }
    }
}