namespace StudyTracker.Application.DTOs.Topic;

public record TopicDto(
    Guid Id,
    Guid SubjectId,
    Guid? ParentTopicId,
    string Name,
    string? Description,
    int Order,
    DateTime CreatedAt
);