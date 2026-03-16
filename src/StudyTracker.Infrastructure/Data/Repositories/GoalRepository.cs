using Microsoft.EntityFrameworkCore;
using StudyTracker.Domain.Entities;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Infrastructure.Data.Repositories;

public class GoalRepository : BaseRepository<Goal>, IGoalRepository
{
    public GoalRepository(AppDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Goal>> GetActiveGoalsAsync(CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(g => g.Streak)
            .Include(g => g.Subject)
            .Where(g => g.IsActive)
            .ToListAsync(ct);
    }
}