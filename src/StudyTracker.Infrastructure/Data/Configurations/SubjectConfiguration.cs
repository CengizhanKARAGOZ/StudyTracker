using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTracker.Domain.Entities;

namespace StudyTracker.Infrastructure.Data.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
        builder.Property(s => s.Color).IsRequired().HasMaxLength(20);
        builder.Property(s => s.Icon).HasMaxLength(50);
        builder.Property(s => s.Description).HasMaxLength(500);

        builder.HasMany(s => s.Topics)
            .WithOne(t => t.Subject)
            .HasForeignKey(t => t.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Goals)
            .WithOne(g => g.Subject)
            .HasForeignKey(g => g.SubjectId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}