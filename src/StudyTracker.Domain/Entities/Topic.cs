using StudyTracker.Domain.Common;

namespace StudyTracker.Domain.Entities;

public class Topic : BaseEntity
{
    public Guid SubjectId { get; set; }
    public Guid? ParentTopicId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Order { get; set; } = 0;
    
    public Subject Subject { get; set; } = null!;
    public Topic? ParentTopic { get; set; }
    public ICollection<Topic> SubTopics { get; set; } = new List<Topic>();
    public ICollection<StudySession> StudySessions { get; set; } = new List<StudySession>();
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}