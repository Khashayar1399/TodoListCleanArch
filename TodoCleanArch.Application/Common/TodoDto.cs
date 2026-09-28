namespace TodoCleanArch.Application.Common;

public record TodoDto(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime CreatedAt
);