using Microsoft.EntityFrameworkCore;
using StudyTracker.Domain.Entities;
using StudyTracker.Domain.Enums;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Infrastructure.Data.Repositories;

public class StudySessionRepository : BaseRepository<StudySession>, IStudySessionRepository
{
    public StudySessionRepository(AppDbContext context) : base(context) { }

    public async Task<IReadOnlyList<StudySession>> GetByDateRangeAsync(DateTime start, DateTime end, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(s => s.Topic)
            .ThenInclude(t => t.Subject)
            .Where(s => s.StartTime >= start && s.StartTime <= end)
            .OrderByDescending(s => s.StartTime)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<StudySession>> GetByTopicAsync(Guid topicId, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(s => s.TopicId == topicId)
            .OrderByDescending(s => s.StartTime)
            .ToListAsync(ct);
    }

    public async Task<StudySession?> GetActiveSessionAsync(CancellationToken ct = default)
    {
        return await _dbSet
            .Include(s => s.Topic)
            .ThenInclude(t => t.Subject)
            .FirstOrDefaultAsync(s => s.Status == SessionStatus.Active || s.Status == SessionStatus.Paused, ct);
    }
}