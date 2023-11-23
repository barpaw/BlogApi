using BlogApi.WebApi.Static;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.WebApi.Controllers.User;

[ApiController]
[Tags("User")]
[Route("users")]
[ApiExplorerSettings(GroupName = ApiGroupName.User)]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }
}