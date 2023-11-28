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


    public void Dispose()
    {
        // TODO release managed resources here
    }
}