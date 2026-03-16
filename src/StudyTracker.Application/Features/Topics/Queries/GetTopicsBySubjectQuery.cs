using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Subject;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Topics.Queries;

public record GetTopicsBySubjectQuery(Guid SubjectId) : IRequest<ApiResponse<List<TopicTreeDto>>>;

public class GetTopicsBySubjectHandler : IRequestHandler<GetTopicsBySubjectQuery, ApiResponse<List<TopicTreeDto>>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetTopicsBySubjectHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<TopicTreeDto>>> Handle(GetTopicsBySubjectQuery request, CancellationToken ct)
    {
        var topics = await _uow.Topics.GetBySubjectAsync(request.SubjectId, ct);
        var dtos = _mapper.Map<List<TopicTreeDto>>(topics);
        return ApiResponse<List<TopicTreeDto>>.Ok(dtos);
    }
}