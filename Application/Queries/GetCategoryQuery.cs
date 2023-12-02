using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Category;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetCategoryQuery(Guid Id) : IRequest<CategoryDto?>;