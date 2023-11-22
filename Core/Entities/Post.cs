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
    
    
    public ICollection<Category>? Categories { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    
    public User User { get; set; } 
}