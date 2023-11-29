using MediatR;

namespace BlogApi.Application.Commands;

public record DeletePostCommand(Guid Id) : IRequest<bool>;