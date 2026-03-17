namespace StudyTracker.Application.DTOs.StudySession;

public record StartSessionDto(
    Guid TopicId,
    string? Description
);