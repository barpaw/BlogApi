using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Category;
using BlogApi.Core.Interfaces.UoW;
using MediatR;

namespace BlogApi.Application.Queries.Handlers;

public sealed class GetCategoryHandler : IRequestHandler<GetCategoryQuery, CategoryDto?>
{
    private readonly ILogger<GetCategoryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryHandler(ILogger<GetCategoryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto?> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);

        return _mapper.Map<CategoryDto?>(category);
    }
}