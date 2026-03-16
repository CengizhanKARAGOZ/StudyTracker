namespace StudyTracker.Domain.Entities;

public class DailyLog
{
    public DateOnly Date { get; set; }
    public int TotalMinutes { get; set; }
    public int SessionCount { get; set; }
    public Guid? TopSubjectId { get; set; }

    public Subject? TopSubject { get; set; }
}