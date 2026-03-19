using StudyTracker.Domain.Enums;

namespace StudyTracker.Application.DTOs.Goal;

public record CreateGoalDto(
    Guid? SubjectId,
    string Title,
    int TargetMinutes,
    GoalType GoalType
    );