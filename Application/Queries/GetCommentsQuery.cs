using BlogApi.Application.DTOs;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetCommentsQuery(QueryParameters queryParams) : IRequest<PagedResult<CommentDto>>;