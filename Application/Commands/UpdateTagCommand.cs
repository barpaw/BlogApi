using BlogApi.Application.DTOs;
using MediatR;

namespace BlogApi.Application.Commands
{
    public record UpdateTagCommand(Guid Id, UpdateTagDto UpdateTagDto) : IRequest<bool>;
}
