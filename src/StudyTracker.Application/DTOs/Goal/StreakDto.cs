namespace StudyTracker.Application.DTOs.Goal;

public record StreakDto(
    int CurrentStreak,
    int LongestStreak,
    DateOnly LastActivityDate
    );