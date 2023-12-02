using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Post;
using BlogApi.Core.Interfaces.UoW;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;

namespace BlogApi.Application.Queries.Handlers;

public sealed class GetPostsHandler : IRequestHandler<GetPostsQuery, PagedResult<PostDto>>
{
    private readonly ILogger<GetPostsHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPostsHandler(ILogger<GetPostsHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<PostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _unitOfWork.Posts.GetAsync(request.queryParams);

        return posts;
    }
}