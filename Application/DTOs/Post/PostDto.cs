namespace BlogApi.Application.DTOs.Post;

public record PostDto(Guid Id, string? Title, string? Content, string UserId, DateTimeOffset PublishedDate,
    bool IsPublished);
    