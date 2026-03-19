using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Note;
using StudyTracker.Domain.Entities;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Notes.Commands;

public record CreateNoteCommand(CreateNoteDto Dto) : IRequest<ApiResponse<NoteDto>>;

public class CreateNoteHandler : IRequestHandler<CreateNoteCommand, ApiResponse<NoteDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateNoteHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<NoteDto>> Handle(CreateNoteCommand request, CancellationToken ct)
    {
        var topicExists = await _uow.Topics.ExistsAsync(request.Dto.TopicId, ct);
        if (!topicExists)
            return ApiResponse<NoteDto>.Fail("Konu bulunamadı");

        if (request.Dto.SessionId.HasValue)
        {
            var sessionExists = await _uow.StudySessions.ExistsAsync(request.Dto.SessionId.Value, ct);
            if (!sessionExists)
                return ApiResponse<NoteDto>.Fail("Oturum bulunamadı");
        }

        var note = _mapper.Map<Note>(request.Dto);
        await _uow.Notes.AddAsync(note, ct);
        await _uow.SaveChangesAsync(ct);

        var dto = _mapper.Map<NoteDto>(note);
        return ApiResponse<NoteDto>.Ok(dto, "Not oluşturuldu");
    }
}