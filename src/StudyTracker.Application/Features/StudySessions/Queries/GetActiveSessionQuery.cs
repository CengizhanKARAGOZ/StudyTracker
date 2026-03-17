using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.StudySession;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.StudySessions.Queries;

public record GetActiveSessionQuery : IRequest<ApiResponse<StudySessionDto?>>;

public class GetActiveSessionHandler : IRequestHandler<GetActiveSessionQuery, ApiResponse<StudySessionDto?>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetActiveSessionHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<StudySessionDto?>> Handle(GetActiveSessionQuery request, CancellationToken ct)
    {
        var session = await _uow.StudySessions.GetActiveSessionAsync(ct);
        if (session is null)
            return ApiResponse<StudySessionDto?>.Ok(null, "Aktif oturum yok");

        var dto = _mapper.Map<StudySessionDto>(session);
        return ApiResponse<StudySessionDto?>.Ok(dto);
    }
}