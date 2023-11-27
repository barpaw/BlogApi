using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Shared.Helpers.Queryable;
using BlogApi.WebApi.Models;

namespace BlogApi.Core.Interfaces.Repositories;

public interface ITagRepository : IDisposable
{
    Task AddAsync(Tag tag);
    Task<PagedResult<TagDto>> GetAsync(GetTagsQueryParameters queryParameters);
    Task<TagDto> GetByIdAsync(Guid id);
    Task<bool> Delete(Guid id);
    Task<bool> Update(TagDto tagDto);
}