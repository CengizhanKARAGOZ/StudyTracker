namespace StudyTracker.Application.DTOs.Subject;

public record CreateSubjectDto(
    string Name,
    string Color,
    string? Icon,
    string? Description
);