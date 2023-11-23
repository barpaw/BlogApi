using Microsoft.AspNetCore.Identity;

namespace BlogApi.Core.Entities;

public class User : IdentityUser
{
    
    public string? Nick { get; set; }
    
    public string? Role { get; set; }
    
    public string? Bio { get; set; }
    
    public virtual ICollection<Post>? Posts { get; set; }
    
    public virtual ICollection<Comment>? Comments { get; set; }
}