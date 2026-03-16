using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTracker.Domain.Entities;

namespace StudyTracker.Infrastructure.Data.Configurations;

public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Title).IsRequired().HasMaxLength(300);
        builder.Property(g => g.GoalType).HasConversion<string>().HasMaxLength(20);

        builder.HasOne(g => g.Streak)
            .WithOne(s => s.Goal)
            .HasForeignKey<Streak>(s => s.GoalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}