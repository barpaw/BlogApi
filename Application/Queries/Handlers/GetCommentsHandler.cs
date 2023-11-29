using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Core.Interfaces.UoW;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;

namespace BlogApi.Application.Queries.Handlers;

public sealed class GetCommentsHandler : IRequestHandler<GetCommentsQuery, PagedResult<CommentDto>>
{
    private readonly ILogger<GetCommentsHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCommentsHandler(ILogger<GetCommentsHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<CommentDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _unitOfWork.Comments.GetAsync(request.queryParams);

        return comments;
    }
}