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

public class PostRepository : IPostRepository
{
    private readonly ILogger<PostRepository> _logger;
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;

    public PostRepository(ILogger<PostRepository> logger, IMapper mapper, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task AddAsync(Post post)
    {
        await _appDbContext.AddAsync(post);
    }

    public async Task<PagedResult<PostDto>> GetAsync(QueryParameters queryParameters)
    {
        var query = _appDbContext.Posts.AsQueryable();

        var retQuery = query.ApplySorting(queryParameters)
            .Select(t => new PostDto(t.Id, t.Title, t.Content, t.UserId, t.PublishedDate, t.IsPublished)).AsQueryable();

        return await retQuery.GetPagedAsync(queryParameters);
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        var entity = await _appDbContext.Posts
            .FirstOrDefaultAsync(t => t.Id == id);
        
        return entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await _appDbContext.Posts.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        _appDbContext.Posts.Remove(entity);

        return true;
    }

    public async Task<bool> Update(Guid id, UpdatePostDto updatePostDto)
    {
        var entity = await _appDbContext.Posts.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        entity.IsPublished = updatePostDto.IsPublished;
        entity.Content = updatePostDto.Content;
        entity.Title = updatePostDto.Title;
        entity.UserId = updatePostDto.UserId;

        return true;
    }


    public void Dispose()
    {
        // TODO release managed resources here
    }
}