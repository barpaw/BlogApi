using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Category;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.UoW;
using MediatR;

namespace BlogApi.Application.Commands.Handlers;

public sealed class AddCategoryHandler : IRequestHandler<AddCategoryCommand, CategoryDto>
{
    private readonly ILogger<AddCategoryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddCategoryHandler(ILogger<AddCategoryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category()
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        await _unitOfWork.Categories.AddAsync(category);

        await _unitOfWork.CommitAsync(cancellationToken);

        var categoryDto = _mapper.Map<CategoryDto>(category);

        return categoryDto;
    }
}
    


