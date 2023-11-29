using MediatR;

namespace BlogApi.Application.Commands;

public record DeleteCategoryCommand(Guid Id) : IRequest<bool>;