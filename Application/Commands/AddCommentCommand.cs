using BlogApi.Application.DTOs;
using MediatR;

namespace BlogApi.Application.Commands;

public record AddCommentCommand(Guid Id, Guid PostId, string? UserId, string? Content ) : IRequest<CommentDto>;