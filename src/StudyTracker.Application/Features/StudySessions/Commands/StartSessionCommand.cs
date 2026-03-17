using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.StudySession;
using StudyTracker.Domain.Entities;
using StudyTracker.Domain.Enums;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.StudySessions.Commands;

public record StartSessionCommand(StartSessionDto Dto) : IRequest<ApiResponse<StudySessionDto>>;

public class StartSessionHandler : IRequestHandler<StartSessionCommand, ApiResponse<StudySessionDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public StartSessionHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<StudySessionDto>> Handle(StartSessionCommand request, CancellationToken ct)
    {
        var active = await _uow.StudySessions.GetActiveSessionAsync(ct);
        if (active is not null)
            return ApiResponse<StudySessionDto>.Fail("Zaten aktif bir oturum var. Önce onu bitirin.");

        var topic = await _uow.Topics.GetWithSubTopicsAsync(request.Dto.TopicId, ct);
        if (topic is null)
            return ApiResponse<StudySessionDto>.Fail("Konu bulunamadı");

        var session = new StudySession
        {
            TopicId = request.Dto.TopicId,
            StartTime = DateTime.UtcNow,
            Status = SessionStatus.Active,
            Description = request.Dto.Description
        };

        await _uow.StudySessions.AddAsync(session, ct);
        await _uow.SaveChangesAsync(ct);

        session.Topic = topic;

        var dto = _mapper.Map<StudySessionDto>(session);
        return ApiResponse<StudySessionDto>.Ok(dto, "Oturum başlatıldı");
    }
}