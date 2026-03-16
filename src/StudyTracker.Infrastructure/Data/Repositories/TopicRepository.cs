using Microsoft.EntityFrameworkCore;
using StudyTracker.Domain.Entities;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Infrastructure.Data.Repositories;

public class TopicRepository : BaseRepository<Topic>, ITopicRepository
{
    public TopicRepository(AppDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Topic>> GetBySubjectAsync(Guid subjectId, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(t => t.SubjectId == subjectId && t.ParentTopicId == null)
            .OrderBy(t => t.Order)
            .Include(t => t.SubTopics.OrderBy(st => st.Order))
            .ToListAsync(ct);
    }

    public async Task<Topic?> GetWithSubTopicsAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbSet
            .Include(t => t.SubTopics.OrderBy(st => st.Order))
            .Include(t => t.Subject)
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }
}