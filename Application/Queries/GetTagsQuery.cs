using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;
using BlogApi.Shared.Helpers.Queryable;
using BlogApi.WebApi.Controllers.Tag;
using BlogApi.WebApi.Models;
using MediatR;

namespace BlogApi.Application.Queries;

public record GetTagsQuery(GetTagsQueryParameters queryParams) : IRequest<PagedResult<Tag>>;