using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public ISubjectRepository Subjects { get; }
    public ITopicRepository Topics { get; }
    public IStudySessionRepository StudySessions { get; }
    public INoteRepository Notes { get; }
    public IGoalRepository Goals { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Subjects = new SubjectRepository(context);
        Topics = new TopicRepository(context);
        StudySessions = new StudySessionRepository(context);
        Notes = new NoteRepository(context);
        Goals = new GoalRepository(context);
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await _context.SaveChangesAsync(ct);

    public void Dispose() => _context.Dispose();
}