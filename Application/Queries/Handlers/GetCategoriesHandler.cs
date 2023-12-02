using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Category;
using BlogApi.Core.Interfaces.UoW;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;

namespace BlogApi.Application.Queries.Handlers;

public sealed class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, PagedResult<CategoryDto>>
{
    private readonly ILogger<GetCategoriesHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoriesHandler(ILogger<GetCategoriesHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Categories.GetAsync(request.queryParams);

        return categories;
    }
}