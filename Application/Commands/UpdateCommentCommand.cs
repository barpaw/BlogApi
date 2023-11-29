using BlogApi.Application.DTOs;
using MediatR;

namespace BlogApi.Application.Commands;

public record UpdateCommentCommand(Guid Id, UpdateCommentDto UpdateCommentDto) : IRequest<bool>;