using BlogApi.Shared.Helpers.Queryable;

namespace BlogApi.WebApi.Models;

public class GetTagsQueryParameters : QueryParameters
{
    public string? Name { get; set; }
}