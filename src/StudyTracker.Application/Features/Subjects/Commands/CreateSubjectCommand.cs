using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Subject;
using StudyTracker.Domain.Entities;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Subjects.Commands;

public record CreateSubjectCommand(CreateSubjectDto Dto) : IRequest<ApiResponse<SubjectDto>>;

public class CreateSubjectHandler : IRequestHandler<CreateSubjectCommand, ApiResponse<SubjectDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateSubjectHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<SubjectDto>> Handle(CreateSubjectCommand request, CancellationToken ct)
    {
        var subject = _mapper.Map<Subject>(request.Dto);
        await _uow.Subjects.AddAsync(subject, ct);
        await _uow.SaveChangesAsync(ct);

        var dto = _mapper.Map<SubjectDto>(subject);
        return ApiResponse<SubjectDto>.Ok(dto, "Ders oluşturuldu");
    }
}