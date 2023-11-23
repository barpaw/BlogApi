namespace BlogApi.Core.Entities;

public class Tag
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public virtual ICollection<Post>? Posts { get; set; }
}