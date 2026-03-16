namespace StudyTracker.Application.DTOs.Subject;

public record UpdateSubjectDto(
    string Name,
    string Color,
    string? Icon,
    string? Description,
    bool IsArchived
);