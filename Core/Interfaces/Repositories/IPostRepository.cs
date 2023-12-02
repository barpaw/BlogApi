using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Post;
using BlogApi.Core.Entities;
using BlogApi.Shared.Helpers.Queryable;

namespace BlogApi.Core.Interfaces.Repositories;

public interface IPostRepository : IDisposable
{
    Task AddAsync(Post post);
    Task<PagedResult<PostDto>> GetAsync(QueryParameters queryParameters);
    Task<Post?> GetByIdAsync(Guid id);
    
    Task<bool> Delete(Guid id);
    Task<bool> Update(Guid id, UpdatePostDto updatePostDto);
}