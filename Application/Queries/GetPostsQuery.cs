using BlogApi.Application.DTOs;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetPostsQuery(QueryParameters queryParams) : IRequest<PagedResult<PostDto>>;