using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Domain.Enums;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.StudySessions.Commands;

public record ResumeSessionCommand(Guid Id) : IRequest<ApiResponse<bool>>;

public class ResumeSessionHandler : IRequestHandler<ResumeSessionCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public ResumeSessionHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(ResumeSessionCommand request, CancellationToken ct)
    {
        var session = await _uow.StudySessions.GetByIdAsync(request.Id, ct);
        if (session is null)
            return ApiResponse<bool>.Fail("Oturum bulunamadı");

        if (session.Status != SessionStatus.Paused)
            return ApiResponse<bool>.Fail("Sadece duraklatılmış oturumlar devam ettirilebilir");

        session.Status = SessionStatus.Active;
        session.StartTime = DateTime.UtcNow;

        await _uow.StudySessions.UpdateAsync(session, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Oturum devam ediyor");
    }
}