namespace BlogApi.Application.DTOs;

public class PostDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string UserId { get; set; }
    public DateTimeOffset PublishedDate { get; set; }
    public bool IsPublished { get; set; }
}