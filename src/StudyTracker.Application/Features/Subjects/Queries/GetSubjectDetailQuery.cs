using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Subject;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Subjects.Queries;

public record GetSubjectDetailQuery(Guid Id) : IRequest<ApiResponse<SubjectDetailDto>>;

public class GetSubjectDetailHandler : IRequestHandler<GetSubjectDetailQuery, ApiResponse<SubjectDetailDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetSubjectDetailHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<SubjectDetailDto>> Handle(GetSubjectDetailQuery request, CancellationToken ct)
    {
        var subject = await _uow.Subjects.GetWithTopicsAsync(request.Id, ct);
        if (subject is null)
            return ApiResponse<SubjectDetailDto>.Fail("Ders bulunamadı");

        var dto = _mapper.Map<SubjectDetailDto>(subject);
        return ApiResponse<SubjectDetailDto>.Ok(dto);
    }
}