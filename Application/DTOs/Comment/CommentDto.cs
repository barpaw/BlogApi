namespace BlogApi.Application.DTOs.Comment;

public record CommentDto(Guid Id, Guid PostId, string? UserId, string? Content, DateTimeOffset PublishedDate);