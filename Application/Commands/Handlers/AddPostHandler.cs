using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.UoW;
using BlogApi.Shared.Constants;
using MediatR;

namespace BlogApi.Application.Commands.Handlers;

public sealed class AddPostHandler : IRequestHandler<AddPostCommand, PostDto>
{
    private readonly ILogger<AddPostHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddPostHandler(ILogger<AddPostHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PostDto> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post()
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            Title = request.Title,
            PublishedDate = DateTimeOffset.Now,
            IsPublished = false,
            UserId = UserHardcoded.UserId
        };

        await _unitOfWork.Posts.AddAsync(post);

        await _unitOfWork.CommitAsync(cancellationToken);

        var postDto = _mapper.Map<PostDto>(post);

        return postDto;
    }
}