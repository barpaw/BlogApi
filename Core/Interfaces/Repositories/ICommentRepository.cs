using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Shared.Helpers.Queryable;

namespace BlogApi.Core.Interfaces.Repositories;

public interface ICommentRepository : IDisposable
{
    Task AddAsync(Comment comment);
    Task<PagedResult<CommentDto>> GetAsync(QueryParameters queryParameters);
    Task<CommentDto> GetByIdAsync(Guid id);
    Task<bool> Delete(Guid id);
    Task<bool> Update(Guid id, UpdateCommentDto update);
}