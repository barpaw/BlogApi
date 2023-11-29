using MediatR;

namespace BlogApi.Application.Commands;

public record DeleteCommentCommand(Guid Id) : IRequest<bool>;