using Microsoft.AspNetCore.Identity;

namespace BlogApi.Core.Entities;

public class Post
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string UserId { get; set; }
    public DateTimeOffset PublishedDate { get; set; }
    public bool IsPublished { get; set; }
    
    
    public virtual ICollection<Category>? Categories { get; set; }
    public virtual ICollection<Tag>? Tags { get; set; }
    
    public virtual User User { get; set; } 
}