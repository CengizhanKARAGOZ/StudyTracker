using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Subject;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Subjects.Queries;

public record GetAllSubjectsQuery : IRequest<ApiResponse<List<SubjectDto>>>;

public class GetAllSubjectsHandler : IRequestHandler<GetAllSubjectsQuery, ApiResponse<List<SubjectDto>>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetAllSubjectsHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<SubjectDto>>> Handle(GetAllSubjectsQuery request, CancellationToken ct)
    {
        var subjects = await _uow.Subjects.FindAsync(s => !s.IsArchived, ct);
        var dtos = _mapper.Map<List<SubjectDto>>(subjects);
        return ApiResponse<List<SubjectDto>>.Ok(dtos);
    }
}