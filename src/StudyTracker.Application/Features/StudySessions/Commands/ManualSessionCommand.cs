using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.StudySession;
using StudyTracker.Domain.Entities;
using StudyTracker.Domain.Enums;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.StudySessions.Commands;

public record ManualSessionCommand(ManualSessionDto Dto) : IRequest<ApiResponse<StudySessionDto>>;

public class ManualSessionHandler : IRequestHandler<ManualSessionCommand, ApiResponse<StudySessionDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ManualSessionHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<StudySessionDto>> Handle(ManualSessionCommand request, CancellationToken ct)
    {
        var dto = request.Dto;

        if (dto.EndTime <= dto.StartTime)
            return ApiResponse<StudySessionDto>.Fail("Bitiş zamanı başlangıçtan sonra olmalı");

        var topic = await _uow.Topics.GetWithSubTopicsAsync(dto.TopicId, ct);
        if (topic is null)
            return ApiResponse<StudySessionDto>.Fail("Konu bulunamadı");

        var duration = (int)(dto.EndTime - dto.StartTime).TotalMinutes;

        var session = new StudySession
        {
            TopicId = dto.TopicId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            DurationMinutes = duration,
            Status = SessionStatus.Completed,
            Rating = dto.Rating,
            Description = dto.Description
        };

        await _uow.StudySessions.AddAsync(session, ct);
        await _uow.SaveChangesAsync(ct);

        session.Topic = topic;
        var result = _mapper.Map<StudySessionDto>(session);
        return ApiResponse<StudySessionDto>.Ok(result, "Oturum manuel olarak eklendi");
    }
}