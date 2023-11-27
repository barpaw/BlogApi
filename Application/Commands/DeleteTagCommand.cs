using MediatR;

namespace BlogApi.Application.Commands;

public record DeleteTagCommand(Guid Id) : IRequest<bool>;