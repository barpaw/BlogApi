using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetTagsQuery(QueryParameters queryParams) : IRequest<PagedResult<Tag>>;