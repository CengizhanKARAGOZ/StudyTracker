using StudyTracker.Domain.Common;

namespace StudyTracker.Domain.Entities;

public class Note : BaseEntity
{
    public Guid TopicId { get; set; }
    public Guid? SessionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsPinned { get; set; } = false;

    public Topic Topic { get; set; } = null!;
    public StudySession? Session { get; set; }
}