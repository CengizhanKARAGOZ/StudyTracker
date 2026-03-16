using StudyTracker.Domain.Common;
using StudyTracker.Domain.Enums;

namespace StudyTracker.Domain.Entities;

public class StudySession : BaseEntity
{
    public Guid TopicId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int DurationMinutes { get; set; }
    public SessionStatus Status { get; set; } = SessionStatus.Active;
    public int? Rating { get; set; }
    public string? Description { get; set; }

    public Topic Topic { get; set; } = null!;
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}