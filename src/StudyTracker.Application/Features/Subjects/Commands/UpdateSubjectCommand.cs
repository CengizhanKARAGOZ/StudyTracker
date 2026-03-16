using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Subject;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Subjects.Commands;

public record UpdateSubjectCommand(Guid Id, UpdateSubjectDto Dto) : IRequest<ApiResponse<bool>>;

public class UpdateSubjectHandler : IRequestHandler<UpdateSubjectCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public UpdateSubjectHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(UpdateSubjectCommand request, CancellationToken ct)
    {
        var subject = await _uow.Subjects.GetByIdAsync(request.Id, ct);
        if (subject is null)
            return ApiResponse<bool>.Fail("Ders bulunamadı");

        subject.Name = request.Dto.Name;
        subject.Color = request.Dto.Color;
        subject.Icon = request.Dto.Icon;
        subject.Description = request.Dto.Description;
        subject.IsArchived = request.Dto.IsArchived;

        await _uow.Subjects.UpdateAsync(subject, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Ders güncellendi");
    }
}