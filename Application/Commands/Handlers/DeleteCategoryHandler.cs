using AutoMapper;
using BlogApi.Core.Interfaces.UoW;
using MediatR;

namespace BlogApi.Application.Commands.Handlers;

public sealed class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ILogger<DeleteCategoryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteCategoryHandler(ILogger<DeleteCategoryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Categories.Delete(request.Id);

        if (result)
        {
            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }

        return false;
    }
}