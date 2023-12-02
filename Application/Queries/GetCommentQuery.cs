using BlogApi.Application.DTOs.Comment;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetCommentQuery(Guid Id) : IRequest<CommentDto?>;