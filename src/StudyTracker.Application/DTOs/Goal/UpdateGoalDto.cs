using StudyTracker.Domain.Enums;

namespace StudyTracker.Application.DTOs.Goal;

public record UpdateGoalDto(
    string Title,
    int TargetMinutes,
    GoalType GoalType,
    bool IsActive
    );