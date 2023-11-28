using BlogApi.Application.DTOs;
using BlogApi.Shared.Helpers.Queryable;
using BlogApi.WebApi.Models;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetTagsQuery(QueryParameters queryParams) : IRequest<PagedResult<TagDto>>;