using BlogApi.Application.DTOs;
using MediatR;

namespace BlogApi.Application.Commands
{
    public record UpdateTagCommand(TagDto TagDto) : IRequest<bool>;
}
