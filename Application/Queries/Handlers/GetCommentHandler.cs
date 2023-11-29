using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Core.Interfaces.UoW;
using MediatR;

namespace BlogApi.Application.Queries.Handlers;

public sealed class GetCommentHandler : IRequestHandler<GetCommentQuery, CommentDto>
{
    private readonly ILogger<GetCommentHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCommentHandler(ILogger<GetCommentHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CommentDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.Comments.GetByIdAsync(request.Id);

        return comment;
    }
}