using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Shared.Helpers.Queryable;

namespace BlogApi.Core.Interfaces.Repositories;

public interface ITagRepository : IDisposable
{
    Task AddAsync(Tag tag);
    Task<PagedResult<Tag>> GetAsync(QueryParameters queryParameters);
}