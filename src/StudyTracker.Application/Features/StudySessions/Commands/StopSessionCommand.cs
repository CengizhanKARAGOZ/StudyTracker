using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.StudySession;
using StudyTracker.Domain.Enums;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.StudySessions.Commands;

public record StopSessionCommand(Guid Id, StopSessionDto Dto) : IRequest<ApiResponse<StudySessionDto>>;

public class StopSessionHandler : IRequestHandler<StopSessionCommand, ApiResponse<StudySessionDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public StopSessionHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<StudySessionDto>> Handle(StopSessionCommand request, CancellationToken ct)
    {
        var session = await _uow.StudySessions.GetByIdAsync(request.Id, ct);
        if (session is null)
            return ApiResponse<StudySessionDto>.Fail("Oturum bulunamadı");

        if (session.Status == SessionStatus.Completed)
            return ApiResponse<StudySessionDto>.Fail("Bu oturum zaten tamamlanmış");

        if (session.Status == SessionStatus.Active)
        {
            session.DurationMinutes += (int)(DateTime.UtcNow - session.StartTime).TotalMinutes;
        }

        session.EndTime = DateTime.UtcNow;
        session.Status = SessionStatus.Completed;
        session.Rating = request.Dto.Rating;

        await _uow.StudySessions.UpdateAsync(session, ct);
        await _uow.SaveChangesAsync(ct);

        var loaded = await _uow.StudySessions.GetByDateRangeAsync(
            session.StartTime.AddMinutes(-1), DateTime.UtcNow, ct);
        var result = loaded.FirstOrDefault(s => s.Id == session.Id);

        var dto = _mapper.Map<StudySessionDto>(result ?? session);
        return ApiResponse<StudySessionDto>.Ok(dto, "Oturum tamamlandı");
    }
}