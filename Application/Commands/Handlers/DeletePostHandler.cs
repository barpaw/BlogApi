using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.UoW;
using MediatR;

namespace BlogApi.Application.Commands.Handlers;

public sealed class DeletePostHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly ILogger<DeletePostHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeletePostHandler(ILogger<DeletePostHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Posts.Delete(request.Id);

        if (result)
        {
            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }

        return false;
    }
}