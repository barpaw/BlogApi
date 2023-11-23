

namespace BlogApi.Shared.Helpers.Queryable;

public class QueryParameters
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public List<OrderCriteria> OrderBy { get; set; } = new List<OrderCriteria>();
    public List<FilterCriteria> Filter { get; set; } = new List<FilterCriteria>();
}