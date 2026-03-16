namespace StudyTracker.Application.DTOs.Topic;

public record CreateTopicDto(
    Guid SubjectId,
    Guid? ParentTopicId,
    string Name,
    string? Description,
    int Order = 0
);
