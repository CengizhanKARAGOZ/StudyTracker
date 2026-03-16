using StudyTracker.Domain.Common;
using StudyTracker.Domain.Enums;

namespace StudyTracker.Domain.Entities;

public class Goal : BaseEntity
{
    public Guid? SubjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int TargetMinutes { get; set; }
    public GoalType GoalType { get; set; } = GoalType.Weekly;
    public bool IsActive { get; set; } = true;

    public Subject? Subject { get; set; }
    public Streak? Streak { get; set; }
}