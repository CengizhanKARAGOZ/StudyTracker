using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTracker.Domain.Entities;

namespace StudyTracker.Infrastructure.Data.Configurations;

public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(200);
        builder.Property(t => t.Description).HasMaxLength(500);

        builder.HasOne(t => t.ParentTopic)
            .WithMany(t => t.SubTopics)
            .HasForeignKey(t => t.ParentTopicId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.StudySessions)
            .WithOne(s => s.Topic)
            .HasForeignKey(s => s.TopicId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Notes)
            .WithOne(n => n.Topic)
            .HasForeignKey(n => n.TopicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}