using Microsoft.EntityFrameworkCore;
using StudyTracker.Domain.Entities;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Infrastructure.Data.Repositories;

public class NoteRepository : BaseRepository<Note>, INoteRepository
{
    public NoteRepository(AppDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Note>> GetByTopicAsync(Guid topicId, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(n => n.TopicId == topicId)
            .OrderByDescending(n => n.IsPinned)
            .ThenByDescending(n => n.CreatedAt)
            .ToListAsync(ct);
    }
}