using BlogApi.Application.DTOs;
using BlogApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.WebApi.Controllers.Comment;

[ApiController]
[Tags("Comment")]
[Route("comment/{Id:guid}")]
[Produces("application/json")]
public class GetComment : ControllerBase
{
    private readonly ILogger<GetComment> _logger;
    private readonly IMediator _mediator;

    public GetComment(ILogger<GetComment> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Get Comment By Id")]
    public async Task<ActionResult<CommentDto>> Get([FromRoute] Guid Id)
    {
        try
        {
            var comment = await _mediator.Send(new GetCommentQuery(Id));
            return Ok(comment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching comment");

            return BadRequest("An error occurred while processing your request.");
        }
    }
}