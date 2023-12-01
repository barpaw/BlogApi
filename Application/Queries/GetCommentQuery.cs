using BlogApi.Application.DTOs;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetCommentQuery(Guid Id) : IRequest<CommentDto?>;