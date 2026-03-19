using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Notes.Commands;

public record DeleteNoteCommand(Guid Id) : IRequest<ApiResponse<bool>>;

public class DeleteNoteHandler : IRequestHandler<DeleteNoteCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public DeleteNoteHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(DeleteNoteCommand request, CancellationToken ct)
    {
        var note = await _uow.Notes.GetByIdAsync(request.Id, ct);
        if (note is null)
            return ApiResponse<bool>.Fail("Not bulunamadı");

        await _uow.Notes.DeleteAsync(note, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Not silindi");
    }
}