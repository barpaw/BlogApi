using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Shared.Helpers.Queryable;
using BlogApi.WebApi.Controllers.Tag;
using BlogApi.WebApi.Models;

namespace BlogApi.Core.Interfaces.Repositories;

public interface ITagRepository : IDisposable
{
    Task AddAsync(Tag tag);
    Task<PagedResult<Tag>> GetAsync(GetTagsQueryParameters queryParameters);
    Task<bool> Delete(Guid id);
}