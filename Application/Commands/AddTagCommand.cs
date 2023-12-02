using BlogApi.Application.DTOs;
using BlogApi.Application.DTOs.Tag;
using BlogApi.Core.Entities;
using MediatR;

namespace BlogApi.Application.Commands;

public record AddTagCommand(string? Name) : IRequest<TagDto>;
