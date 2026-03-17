namespace StudyTracker.Application.DTOs.StudySession;

public record ManualSessionDto(
    Guid TopicId,
    DateTime StartTime,
    DateTime EndTime,
    int? Rating,
    string? Description
);