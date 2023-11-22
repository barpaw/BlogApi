namespace BlogApi.Core.Entities;

public class Tag
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public ICollection<Post>? Posts { get; set; }
}