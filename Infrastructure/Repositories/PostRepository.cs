using BlogApi.Core.Interfaces;
using BlogApi.Core.Interfaces.Repositories;

namespace BlogApi.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    public void Dispose()
    {
        // TODO release managed resources here
    }
}