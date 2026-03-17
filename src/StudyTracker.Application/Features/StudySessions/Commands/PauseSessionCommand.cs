using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Domain.Enums;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.StudySessions.Commands;

public record PauseSessionCommand(Guid Id) : IRequest<ApiResponse<bool>>;

public class PauseSessionHandler : IRequestHandler<PauseSessionCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public PauseSessionHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(PauseSessionCommand request, CancellationToken ct)
    {
        var session = await _uow.StudySessions.GetByIdAsync(request.Id, ct);
        if (session is null)
            return ApiResponse<bool>.Fail("Oturum bulunamadı");

        if (session.Status != SessionStatus.Active)
            return ApiResponse<bool>.Fail("Sadece aktif oturumlar duraklatılabilir");

        session.Status = SessionStatus.Paused;
        session.DurationMinutes += (int)(DateTime.UtcNow - session.StartTime).TotalMinutes;

        await _uow.StudySessions.UpdateAsync(session, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Oturum duraklatıldı");
    }
}