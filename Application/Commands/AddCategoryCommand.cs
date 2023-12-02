using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Category;
using MediatR;

namespace BlogApi.Application.Commands;

public record AddCategoryCommand(string? Name) : IRequest<CategoryDto>;