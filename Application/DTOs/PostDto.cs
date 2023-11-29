namespace BlogApi.Application.DTOs;

public record PostDto(Guid Id, string? Title, string? Content, string UserId, DateTimeOffset PublishedDate,
    bool IsPublished);
    