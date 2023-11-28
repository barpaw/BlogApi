using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.Repositories;
using BlogApi.Infrastructure.Data;
using BlogApi.Shared.Extensions.Queryable;
using BlogApi.Shared.Helpers.Queryable;
using BlogApi.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly ILogger<TagRepository> _logger;
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;

    public TagRepository(ILogger<TagRepository> logger, IMapper mapper, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task AddAsync(Tag tag)
    {
        await _appDbContext.AddAsync(tag);
    }

    public async Task<PagedResult<TagDto>> GetAsync(QueryParameters queryParameters)
    {
        var query = _appDbContext.Tags.AsQueryable();

        var retQuery = query.ApplySorting(queryParameters).Select(t => new TagDto(t.Id, t.Name)).AsQueryable();

        return await retQuery.GetPagedAsync(queryParameters);
    }


    public async Task<TagDto> GetByIdAsync(Guid id)
    {
        var tag = await _appDbContext.Tags
            .FirstOrDefaultAsync(t => t.Id == id);

        return _mapper.Map<TagDto>(tag);
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

    public async Task<bool> Update(TagDto tagDto)
    {
        var entity = await _appDbContext.Tags.FindAsync(tagDto.Id);

        if (entity == null)
        {
            return false;
        }

        entity.Name = tagDto.Name;

        return true;
    }


    public void Dispose()
    {
        // TODO release managed resources here
    }
}