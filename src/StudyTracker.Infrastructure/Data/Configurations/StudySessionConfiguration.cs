using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTracker.Domain.Entities;

namespace StudyTracker.Infrastructure.Data.Configurations;

public class StudySessionConfiguration : IEntityTypeConfiguration<StudySession>
{
    public void Configure(EntityTypeBuilder<StudySession> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Description).HasMaxLength(500);
        builder.Property(s => s.Status).HasConversion<string>().HasMaxLength(20);

        builder.HasMany(s => s.Notes)
            .WithOne(n => n.Session)
            .HasForeignKey(n => n.SessionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}