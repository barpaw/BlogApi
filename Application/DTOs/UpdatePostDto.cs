using Swashbuckle.AspNetCore.Annotations;

namespace BlogApi.Application.DTOs;

public record UpdatePostDto(string? Title, string? Content, string UserId, DateTimeOffset PublishedDate,
    bool IsPublished);
    