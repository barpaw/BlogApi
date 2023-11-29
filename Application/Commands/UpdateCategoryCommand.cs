using BlogApi.Application.DTOs;
using MediatR;

namespace BlogApi.Application.Commands;

public record UpdateCategoryCommand(Guid Id, UpdateCategoryDto UpdateCategoryDto) : IRequest<bool>;