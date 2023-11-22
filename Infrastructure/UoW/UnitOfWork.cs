using BlogApi.Core.Interfaces.Repositories;
using BlogApi.Core.Interfaces.UoW;
using BlogApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    public ICategoryRepository Categories { get; }
    public ICommentRepository Comments { get; }
    public IPostRepository Posts { get; }
    public ITagRepository Tags { get; }
    public IUserRepository Users{ get; }


    private readonly ILogger<UnitOfWork> _logger;
    private AppDbContext _dbContext { get; set; }


    public UnitOfWork(ILogger<UnitOfWork> logger, AppDbContext dbContext, ICategoryRepository categoryRepository,
        ICommentRepository commentRepository, ITagRepository tagRepository, IUserRepository userRepository,
        IPostRepository postRepository)
    {
        _logger = logger;
        _dbContext = dbContext;
        Categories= categoryRepository;
        Comments = commentRepository;
        Posts = postRepository;
        Tags = tagRepository;
        Users = userRepository;
    }


    public int Commit()
    {
        var retValue = _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();

        return retValue;
    }

    public async Task<int> CommitAsync()
    {
        var retValue = await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        return retValue;
    }


    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        var retValue = await _dbContext.SaveChangesAsync(cancellationToken);
        _dbContext.ChangeTracker.Clear();

        return retValue;
    }


    public void Rollback()
    {
        foreach (var entry in _dbContext.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
            }
        }
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }
}