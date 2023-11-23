using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces.UoW;
using MediatR;

namespace BlogApi.Application.Commands.Handlers;

public sealed class AddTagHandler : IRequestHandler<AddTagCommand, TagDto>
{
    private readonly ILogger<AddTagHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddTagHandler(ILogger<AddTagHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TagDto> Handle(AddTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new Tag()
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        await _unitOfWork.Tags.AddAsync(tag);

        await _unitOfWork.CommitAsync(cancellationToken);

        var tagDto = _mapper.Map<TagDto>(tag);

        return tagDto;
    }
}