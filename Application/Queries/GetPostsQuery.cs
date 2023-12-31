using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Post;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetPostsQuery(QueryParameters queryParams) : IRequest<PagedResult<PostDto>>;