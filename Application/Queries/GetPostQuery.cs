using BlogApi.Application.DTOs;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetPostQuery(Guid Id) : IRequest<PostDto?>;