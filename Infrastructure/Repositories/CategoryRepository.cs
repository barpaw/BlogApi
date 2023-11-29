using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.Repositories;
using BlogApi.Infrastructure.Data;
using BlogApi.Shared.Extensions.Queryable;
using BlogApi.Shared.Helpers.Queryable;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ILogger<CategoryRepository> _logger;
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;

    public CategoryRepository(ILogger<CategoryRepository> logger, IMapper mapper, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task AddAsync(Category category)
    {
        await _appDbContext.AddAsync(category);
    }

    public async Task<PagedResult<CategoryDto>> GetAsync(QueryParameters queryParameters)
    {
        var query = _appDbContext.Categories.AsQueryable();

        var retQuery = query.ApplySorting(queryParameters).Select(t => new CategoryDto(t.Id, t.Name)).AsQueryable();

        return await retQuery.GetPagedAsync(queryParameters);
    }


    public async Task<CategoryDto> GetByIdAsync(Guid id)
    {
        var tag = await _appDbContext.Categories
            .FirstOrDefaultAsync(t => t.Id == id);

        return _mapper.Map<CategoryDto>(tag);
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await _appDbContext.Categories.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        _appDbContext.Categories.Remove(entity);

        return true;
    }

    public async Task<bool> Update(Guid id, UpdateCategoryDto updateCategoryDto)
    {
        var entity = await _appDbContext.Categories.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        entity.Name = updateCategoryDto.Name;

        return true;
    }


    public void Dispose()
    {
        // TODO release managed resources here
    }
}