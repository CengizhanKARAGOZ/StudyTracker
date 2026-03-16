using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTracker.Domain.Entities;

namespace StudyTracker.Infrastructure.Data.Configurations;

public class DailyLogConfiguration : IEntityTypeConfiguration<DailyLog>
{
    public void Configure(EntityTypeBuilder<DailyLog> builder)
    {
        builder.HasKey(d => d.Id);
        builder.HasIndex(d => d.Date).IsUnique();

        builder.HasOne(d => d.TopSubject)
            .WithMany()
            .HasForeignKey(d => d.TopSubjectId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}