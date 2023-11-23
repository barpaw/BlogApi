namespace BlogApi.Core.Entities;

public class Comment
{
    public Guid Id { get; set; }

    public Guid PostId { get; set; }

    public string? UserId { get; set; }

    public string? Content { get; set; }

    public DateTimeOffset PublishedDate { get; set; }

    public virtual User User { get; set; }
}