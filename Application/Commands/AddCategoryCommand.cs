using BlogApi.Application.DTOs;
using MediatR;

namespace BlogApi.Application.Commands;

public record AddCategoryCommand(string? Name) : IRequest<CategoryDto>;