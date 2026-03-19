using StudyTracker.Domain.Enums;

namespace StudyTracker.Application.DTOs.Goal;

public record GoalDto(
    Guid Id,
    Guid? SubjectId,
    string? SubjectName,
    string Title,
    int TargetMinutes,
    GoalType GoalType,
    bool IsActive,
    DateTime CreatedAt,
    StreakDto? Streak
    );