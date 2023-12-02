using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Category;
using BlogApi.Core.Entities;
using BlogApi.Shared.Helpers.Queryable;

namespace BlogApi.Core.Interfaces.Repositories;

public interface ICategoryRepository : IDisposable
{
    Task AddAsync(Category category);
    Task<PagedResult<CategoryDto>> GetAsync(QueryParameters queryParameters);
    Task<Category?> GetByIdAsync(Guid id);
    Task<bool> Delete(Guid id);
    Task<bool> Update(Guid id, UpdateCategoryDto updateCategoryDto);
}