namespace BlogApi.Application.DTOs.Post;

public record UpdatePostDto(string? Title, string? Content, string UserId, DateTimeOffset PublishedDate,
    bool IsPublished);
    