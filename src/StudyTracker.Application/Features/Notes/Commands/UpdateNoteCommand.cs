using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Note;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Notes.Commands;

public record UpdateNoteCommand(Guid Id, UpdateNoteDto Dto) : IRequest<ApiResponse<bool>>;

public class UpdateNoteHandler : IRequestHandler<UpdateNoteCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public UpdateNoteHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(UpdateNoteCommand request, CancellationToken ct)
    {
        var note = await _uow.Notes.GetByIdAsync(request.Id, ct);
        if (note is null)
            return ApiResponse<bool>.Fail("Not bulunamadı");

        note.Title = request.Dto.Title;
        note.Content = request.Dto.Content;

        await _uow.Notes.UpdateAsync(note, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Not güncellendi");
    }
}