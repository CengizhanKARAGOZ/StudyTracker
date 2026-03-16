using Microsoft.EntityFrameworkCore;
using StudyTracker.Domain.Entities;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Infrastructure.Data.Repositories;

public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
{
    public SubjectRepository(AppDbContext context) : base(context) { }

    public async Task<Subject?> GetWithTopicsAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbSet
            .Include(s => s.Topics.Where(t => t.ParentTopicId == null).OrderBy(t => t.Order))
            .ThenInclude(t => t.SubTopics.OrderBy(st => st.Order))
            .FirstOrDefaultAsync(s => s.Id == id, ct);
    }
}