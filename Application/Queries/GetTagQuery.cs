using BlogApi.Application.DTOs;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetTagQuery(Guid Id) : IRequest<TagDto>;