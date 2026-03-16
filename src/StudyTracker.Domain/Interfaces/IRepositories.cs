using System.Linq.Expressions;
using StudyTracker.Domain.Common;
using StudyTracker.Domain.Entities;

namespace StudyTracker.Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    Task<T> AddAsync(T entity, CancellationToken ct = default);
    Task UpdateAsync(T entity, CancellationToken ct = default);
    Task DeleteAsync(T entity, CancellationToken ct = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
}

public interface ISubjectRepository : IRepository<Subject>
{
    Task<Subject?> GetWithTopicsAsync(Guid id, CancellationToken ct = default);
}

public interface ITopicRepository : IRepository<Topic>
{
    Task<IReadOnlyList<Topic>> GetBySubjectAsync(Guid subjectId, CancellationToken ct = default);
    Task<Topic?> GetWithSubTopicsAsync(Guid id, CancellationToken ct = default);
}

public interface IStudySessionRepository : IRepository<StudySession>
{
    Task<IReadOnlyList<StudySession>> GetByDateRangeAsync(DateTime start, DateTime end, CancellationToken ct = default);
    Task<IReadOnlyList<StudySession>> GetByTopicAsync(Guid topicId, CancellationToken ct = default);
    Task<StudySession?> GetActiveSessionAsync(CancellationToken ct = default);
}

public interface INoteRepository : IRepository<Note>
{
    Task<IReadOnlyList<Note>> GetByTopicAsync(Guid topicId, CancellationToken ct = default);
}

public interface IGoalRepository : IRepository<Goal>
{
    Task<IReadOnlyList<Goal>> GetActiveGoalsAsync(CancellationToken ct = default);
}

public interface IUnitOfWork : IDisposable
{
    ISubjectRepository Subjects { get; }
    ITopicRepository Topics { get; }
    IStudySessionRepository StudySessions { get; }
    INoteRepository Notes { get; }
    IGoalRepository Goals { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}