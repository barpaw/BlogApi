using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.UoW;
using BlogApi.Shared.Constants;
using MediatR;

namespace BlogApi.Application.Commands.Handlers;

public sealed class AddCommentHandler : IRequestHandler<AddCommentCommand, CommentDto>
{
    private readonly ILogger<AddCommentHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddCommentHandler(ILogger<AddCommentHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CommentDto> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Comment()
        {
            Id = Guid.NewGuid(),
            PostId = request.PostId,
            UserId = UserHardcoded.UserId,
            Content = request.Content,
            PublishedDate = DateTimeOffset.Now
        };

        await _unitOfWork.Comments.AddAsync(comment);

        await _unitOfWork.CommitAsync(cancellationToken);

        var commentDto = _mapper.Map<CommentDto>(comment);

        return commentDto;
    }
}