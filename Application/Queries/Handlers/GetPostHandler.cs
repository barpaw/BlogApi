using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Post;
using BlogApi.Core.Interfaces.UoW;
using MediatR;

namespace BlogApi.Application.Queries.Handlers;

public sealed class GetPostHandler : IRequestHandler<GetPostQuery, PostDto?>
{
    private readonly ILogger<GetPostHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPostHandler(ILogger<GetPostHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PostDto?> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = await _unitOfWork.Posts.GetByIdAsync(request.Id);

        return _mapper.Map<PostDto>(post);
    }
}