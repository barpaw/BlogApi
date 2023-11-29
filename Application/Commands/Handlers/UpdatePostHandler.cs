using AutoMapper;
using BlogApi.Core.Interfaces.UoW;
using BlogApi.Shared.Constants;
using MediatR;

namespace BlogApi.Application.Commands.Handlers;

public sealed class UpdatePostHandler : IRequestHandler<UpdatePostCommand, bool>
{
    private readonly ILogger<UpdatePostHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdatePostHandler(ILogger<UpdatePostHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Posts.Update(request.Id, request.UpdatePostDto);

        if (result)
        {
            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }

        return false;
    }
}