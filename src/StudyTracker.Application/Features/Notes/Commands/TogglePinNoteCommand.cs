using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Notes.Commands;

public record TogglePinNoteCommand(Guid Id) : IRequest<ApiResponse<bool>>;

public class TogglePinNoteHandler : IRequestHandler<TogglePinNoteCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public TogglePinNoteHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(TogglePinNoteCommand request, CancellationToken ct)
    {
        var note = await _uow.Notes.GetByIdAsync(request.Id, ct);
        if (note is null)
            return ApiResponse<bool>.Fail("Not bulunamadı");

        note.IsPinned = !note.IsPinned;

        await _uow.Notes.UpdateAsync(note, ct);
        await _uow.SaveChangesAsync(ct);

        var message = note.IsPinned ? "Not sabitlendi" : "Not sabitleme kaldırıldı";
        return ApiResponse<bool>.Ok(true, message);
    }
}