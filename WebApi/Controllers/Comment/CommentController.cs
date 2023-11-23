using BlogApi.WebApi.Static;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.WebApi.Controllers.Comment;

[ApiController]
[Tags("Comment")]
[Route("comments")]
[ApiExplorerSettings(GroupName = ApiGroupName.Comment)]
public class CommentController : ControllerBase
{
    private readonly ILogger<CommentController> _logger;

    public CommentController(ILogger<CommentController> logger)
    {
        _logger = logger;
    }
}