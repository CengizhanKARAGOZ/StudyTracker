using StudyTracker.Domain.Common;

namespace StudyTracker.Domain.Entities;

public class Streak : BaseEntity
{
    public Guid GoalId { get; set; }
    public int CurrentStreak { get; set; } = 0;
    public int LongestStreak { get; set; } = 0;
    public DateOnly LastActivityDate { get; set; }

    public Goal Goal { get; set; } = null!;
}