using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Post;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetPostQuery(Guid Id) : IRequest<PostDto?>;