namespace StudyTracker.Application.DTOs.Subject;

public record TopicTreeDto(
    Guid Id,
    string Name,
    string? Description,
    int Order,
    Guid? ParentTopicId,
    List<TopicTreeDto> SubTopics
);