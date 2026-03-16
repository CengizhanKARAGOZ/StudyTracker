using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Topic;
using StudyTracker.Domain.Entities;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Topics.Commands;

public record CreateTopicCommand(CreateTopicDto Dto) : IRequest<ApiResponse<TopicDto>>;

public class CreateTopicHandler : IRequestHandler<CreateTopicCommand, ApiResponse<TopicDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateTopicHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<TopicDto>> Handle(CreateTopicCommand request, CancellationToken ct)
    {
        var subjectExists = await _uow.Subjects.ExistsAsync(request.Dto.SubjectId, ct);
        if (!subjectExists)
            return ApiResponse<TopicDto>.Fail("Ders bulunamadı");

        if (request.Dto.ParentTopicId.HasValue)
        {
            var parentExists = await _uow.Topics.ExistsAsync(request.Dto.ParentTopicId.Value, ct);
            if (!parentExists)
                return ApiResponse<TopicDto>.Fail("Üst konu bulunamadı");
        }

        var topic = _mapper.Map<Topic>(request.Dto);
        await _uow.Topics.AddAsync(topic, ct);
        await _uow.SaveChangesAsync(ct);

        var dto = _mapper.Map<TopicDto>(topic);
        return ApiResponse<TopicDto>.Ok(dto, "Konu oluşturuldu");
    }
}