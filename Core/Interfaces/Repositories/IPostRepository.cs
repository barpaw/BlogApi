using BlogApi.Core.Entities;

namespace BlogApi.Core.Interfaces.Repositories;

public interface IPostRepository : IDisposable
{
    Task AddAsync(Post post);
}