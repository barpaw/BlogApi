using Microsoft.AspNetCore.Mvc;

namespace BlogApi.WebApi.Controllers.Post;

[ApiController]
[Tags("Post")]
[Route("posts")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;

    public PostController(ILogger<PostController> logger)
    {
        _logger = logger;
    }
}