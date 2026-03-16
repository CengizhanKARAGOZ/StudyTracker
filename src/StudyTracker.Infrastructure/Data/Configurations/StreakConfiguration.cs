using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTracker.Domain.Entities;

namespace StudyTracker.Infrastructure.Data.Configurations;

public class StreakConfiguration : IEntityTypeConfiguration<Streak>
{
    public void Configure(EntityTypeBuilder<Streak> builder)
    {
        builder.HasKey(s => s.Id);
    }
}