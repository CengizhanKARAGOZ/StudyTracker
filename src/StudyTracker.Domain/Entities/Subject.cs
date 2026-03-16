using StudyTracker.Domain.Common;

namespace StudyTracker.Domain.Entities;

public class Subject : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#6366f1";
    public string? Icon { get; set; }
    public string? Description { get; set; }
    public bool IsArchived { get; set; } = false;
    
    public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
}