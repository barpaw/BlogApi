using BlogApi.WebApi.Static;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.WebApi.Controllers.Category;

[ApiController]
[Tags("Category")]
[Route("categories")]
[ApiExplorerSettings(GroupName = ApiGroupName.Category)]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ILogger<CategoryController> logger)
    {
        _logger = logger;
    }
    
    // Dodaj Kategorie
    
    // Usun Kategorie
    
    // Wyswietl Kategorie
}