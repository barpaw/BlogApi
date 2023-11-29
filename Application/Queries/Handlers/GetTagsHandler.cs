using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Core.Interfaces.UoW;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;

namespace BlogApi.Application.Queries.Handlers;

public sealed class GetTagsHandler : IRequestHandler<GetTagsQuery, PagedResult<TagDto>>
{
    private readonly ILogger<GetTagsHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTagsHandler(ILogger<GetTagsHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _unitOfWork.Tags.GetAsync(request.queryParams);

        return tags;
    }
}