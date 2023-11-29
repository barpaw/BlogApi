using AutoMapper;
using BlogApi.Core.Interfaces.UoW;
using MediatR;

namespace BlogApi.Application.Commands.Handlers;

public sealed class UpdateTagHandler : IRequestHandler<UpdateTagCommand, bool>
{
    private readonly ILogger<UpdateTagHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTagHandler(ILogger<UpdateTagHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Tags.Update(request.Id, request.UpdateTagDto);

        if (result)
        {
            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }

        return false;
    }
}