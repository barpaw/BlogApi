using AutoMapper;
using BlogApi.Application.DTOs.Comment;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.Repositories;
using BlogApi.Infrastructure.Data;
using BlogApi.Shared.Extensions.Queryable;
using BlogApi.Shared.Helpers.Queryable;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ILogger<CommentRepository> _logger;
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;

    public CommentRepository(ILogger<CommentRepository> logger, IMapper mapper, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task AddAsync(Comment comment)
    {
        await _appDbContext.AddAsync(comment);
    }

    public async Task<PagedResult<CommentDto>> GetAsync(QueryParameters queryParameters)
    {
        var query = _appDbContext.Comments.AsQueryable();

        var retQuery = query.ApplySorting(queryParameters)
            .Select(t => new CommentDto(t.Id, t.PostId, t.UserId, t.Content, t.PublishedDate)).AsQueryable();

        return await retQuery.GetPagedAsync(queryParameters);
    }

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        var entity = await _appDbContext.Comments
            .FirstOrDefaultAsync(t => t.Id == id);

        return entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await _appDbContext.Comments.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        _appDbContext.Comments.Remove(entity);

        return true;
    }

    public async Task<bool> Update(Guid id, UpdateCommentDto updateCommentDto)
    {
        var entity = await _appDbContext.Comments.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        entity.Content = updateCommentDto.Content;

        return true;
    }


    public void Dispose()
    {
        // TODO release managed resources here
    }
}