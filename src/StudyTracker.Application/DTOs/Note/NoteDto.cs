namespace StudyTracker.Application.DTOs.Note;

public record NoteDto(
    Guid Id,
    Guid TopicId,
    string TopicName,
    Guid? SessionId,
    string Title,
    string Content,
    bool IsPinned,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);