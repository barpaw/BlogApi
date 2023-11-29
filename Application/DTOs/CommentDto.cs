namespace BlogApi.Application.DTOs;

public record CommentDto(Guid Id, Guid PostId, string? UserId, string? Content, DateTimeOffset PublishedDate);