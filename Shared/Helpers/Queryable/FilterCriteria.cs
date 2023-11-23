namespace BlogApi.Shared.Helpers.Queryable;

public class FilterCriteria
{
    public string FieldName { get; set; }
    public string Operation { get; set; }
    public object Value { get; set; }
}