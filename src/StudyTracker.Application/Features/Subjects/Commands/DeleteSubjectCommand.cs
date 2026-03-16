using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Subjects.Commands;

public record DeleteSubjectCommand(Guid Id) : IRequest<ApiResponse<bool>>;

public class DeleteSubjectHandler : IRequestHandler<DeleteSubjectCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public DeleteSubjectHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(DeleteSubjectCommand request, CancellationToken ct)
    {
        var subject = await _uow.Subjects.GetByIdAsync(request.Id, ct);
        if (subject is null)
            return ApiResponse<bool>.Fail("Ders bulunamadı");

        subject.IsArchived = true;
        await _uow.Subjects.UpdateAsync(subject, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Ders arşivlendi");
    }
}