using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.Repositories;
using BlogApi.Infrastructure.Data;
using BlogApi.Shared.Extensions.Queryable;
using BlogApi.Shared.Helpers.Queryable;
using BlogApi.WebApi.Controllers.Tag;
using BlogApi.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly ILogger<TagRepository> _logger;
    private readonly AppDbContext _appDbContext;

    public TagRepository(ILogger<TagRepository> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(Tag tag)
    {
        await _appDbContext.AddAsync(tag);
    }

    public async Task<PagedResult<Tag>> GetAsync(GetTagsQueryParameters queryParameters)
    {
        var query = _appDbContext.Tags.AsQueryable();

        return await query.GetPagedAsync(queryParameters);
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await _appDbContext.Tags.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        _appDbContext.Tags.Remove(entity);

        return true;
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}