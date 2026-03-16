namespace StudyTracker.Application.DTOs.Subject;

public record SubjectDetailDto(
    Guid Id,
    string Name,
    string Color,
    string? Icon,
    string? Description,
    bool IsArchived,
    DateTime CreatedAt,
    List<TopicTreeDto> Topics
);