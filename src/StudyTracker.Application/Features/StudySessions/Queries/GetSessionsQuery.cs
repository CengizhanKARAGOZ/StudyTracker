using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.StudySession;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.StudySessions.Queries;

public record GetSessionsQuery(DateTime? From, DateTime? To, Guid? TopicId) : IRequest<ApiResponse<List<StudySessionDto>>>;

public class GetSessionsHandler : IRequestHandler<GetSessionsQuery, ApiResponse<List<StudySessionDto>>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetSessionsHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<StudySessionDto>>> Handle(GetSessionsQuery request, CancellationToken ct)
    {
        if (request.TopicId.HasValue)
        {
            var byTopic = await _uow.StudySessions.GetByTopicAsync(request.TopicId.Value, ct);
            return ApiResponse<List<StudySessionDto>>.Ok(_mapper.Map<List<StudySessionDto>>(byTopic));
        }

        var from = request.From ?? DateTime.UtcNow.AddDays(-30);
        var to = request.To ?? DateTime.UtcNow;

        var sessions = await _uow.StudySessions.GetByDateRangeAsync(from, to, ct);
        var dtos = _mapper.Map<List<StudySessionDto>>(sessions);
        return ApiResponse<List<StudySessionDto>>.Ok(dtos);
    }
}