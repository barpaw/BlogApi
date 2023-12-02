using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Category;
using BlogApi.Shared.Helpers.Queryable;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetCategoriesQuery(QueryParameters queryParams) : IRequest<PagedResult<CategoryDto>>;