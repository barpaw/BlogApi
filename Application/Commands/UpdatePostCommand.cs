using BlogApi.Application.DTOs;
using MediatR;

namespace BlogApi.Application.Commands;

public record UpdatePostCommand(Guid Id, UpdatePostDto UpdatePostDto) : IRequest<bool>;