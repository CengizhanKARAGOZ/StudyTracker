using StudyTracker.Domain.Enums;

namespace StudyTracker.Application.DTOs.StudySession;

public record StudySessionDto(
    Guid Id,
    Guid TopicId,
    string TopicName,
    string SubjectName,
    string SubjectColor,
    DateTime StartTime,
    DateTime? EndTime,
    int DurationMinutes,
    SessionStatus Status,
    int? Rating,
    string? Description,
    DateTime CreatedAt
);