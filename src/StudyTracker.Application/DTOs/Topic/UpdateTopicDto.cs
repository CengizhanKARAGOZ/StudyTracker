namespace StudyTracker.Application.DTOs.Topic;

public record UpdateTopicDto(
    string Name,
    string? Description,
    int Order
);