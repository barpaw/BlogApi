using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Tag;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetTagQuery(Guid Id) : IRequest<TagDto?>;