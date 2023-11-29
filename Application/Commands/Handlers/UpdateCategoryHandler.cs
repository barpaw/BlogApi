using AutoMapper;
using BlogApi.Core.Interfaces.UoW;
using MediatR;

namespace BlogApi.Application.Commands.Handlers;

public sealed class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ILogger<UpdateCategoryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryHandler(ILogger<UpdateCategoryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Categories.Update(request.Id, request.UpdateCategoryDto);

        if (result)
        {
            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }

        return false;
    }
}