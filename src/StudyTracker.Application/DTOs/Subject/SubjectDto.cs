namespace StudyTracker.Application.DTOs.Subject;

public record SubjectDto(
    Guid Id,
    string Name,
    string Color,
    string? Icon,
    string? Description,
    bool IsArchived,
    int TopicCount,
    DateTime CreatedAt
);
