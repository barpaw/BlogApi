using System.ComponentModel;

namespace BlogApi.Shared.Helpers.Queryable;

public class QueryParameters
{

    [DefaultValue("name")]
    public string OrderBy { get; set; } = "Id";
    [DefaultValue("asc")]
    public string OrderDirection { get; set; } = "desc";
    [DefaultValue("1")]
    public int Page { get; set; } = 1;
    [DefaultValue("10")]
    public int PageSize { get; set; } = 10;
    
}