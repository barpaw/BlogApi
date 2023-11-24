namespace BlogApi.Shared.Helpers.Queryable;

public class FilterCriteria
{
    public string Field { get; set; }
    public string Operation { get; set; }
    public object Value { get; set; }
}