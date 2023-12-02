using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Post;
using MediatR;

namespace BlogApi.Application.Commands;

public record AddPostCommand(Guid Id, string? Title, string? Content ) : IRequest<PostDto>;