using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.Repositories;

namespace BlogApi.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository, IRepositoryMarker<Category>
{
    public void Dispose()
    {
        // TODO release managed resources here
    }
}