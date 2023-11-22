using BlogApi.Core.Interfaces.Repositories;

namespace BlogApi.Core.Interfaces.UoW;

public interface IUnitOfWork
{
    public ICategoryRepository Categories { get; }
    public ICommentRepository Comments { get; }
    public IPostRepository Posts { get; }
    public ITagRepository Tags { get; }
    public IUserRepository Users{ get; }


    int Commit();
    Task<int> CommitAsync();
    Task<int> CommitAsync(CancellationToken cancellationToken);
    void Rollback();
}