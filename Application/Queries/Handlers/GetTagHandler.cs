using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Core.Interfaces.UoW;
using MediatR;

namespace BlogApi.Application.Queries.Handlers;

public sealed class GetTagHandler : IRequestHandler<GetTagQuery, TagDto?>
{
    private readonly ILogger<GetTagHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTagHandler(ILogger<GetTagHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TagDto?> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await _unitOfWork.Tags.GetByIdAsync(request.Id);

        return _mapper.Map<TagDto>(tag);
    }
}