using AutoMapper;
using BlogApi.Core.Interfaces.UoW;
using BlogApi.Shared.Constants;
using MediatR;

namespace BlogApi.Application.Commands.Handlers;

public sealed class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, bool>
{
    private readonly ILogger<UpdateCommentHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCommentHandler(ILogger<UpdateCommentHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Comments.Update(request.Id, request.UpdateCommentDto);

        if (result)
        {
            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }

        return false;
    }
}