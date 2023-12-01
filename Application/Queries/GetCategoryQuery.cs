using BlogApi.Application.DTOs;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetCategoryQuery(Guid Id) : IRequest<CategoryDto?>;