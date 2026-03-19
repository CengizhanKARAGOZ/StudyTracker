namespace StudyTracker.Application.DTOs.Note;

public record CreateNoteDto(
    Guid TopicId,
    Guid? SessionId,
    string Title,
    string Content
    );