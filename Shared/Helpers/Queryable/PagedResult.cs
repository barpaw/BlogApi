namespace BlogApi.Shared.Helpers.Queryable;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;

    public string? OrderBy { get; set; }
    public string? OrderDirection { get; set; }
}