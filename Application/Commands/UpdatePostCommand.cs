using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Post;
using MediatR;

namespace BlogApi.Application.Commands;

public record UpdatePostCommand(Guid Id, UpdatePostDto UpdatePostDto) : IRequest<bool>;