using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Topic;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Topics.Commands;

public record UpdateTopicCommand(Guid Id, UpdateTopicDto Dto) : IRequest<ApiResponse<bool>>;

public class UpdateTopicHandler : IRequestHandler<UpdateTopicCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public UpdateTopicHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(UpdateTopicCommand request, CancellationToken ct)
    {
        var topic = await _uow.Topics.GetByIdAsync(request.Id, ct);
        if (topic is null)
            return ApiResponse<bool>.Fail("Konu bulunamadı");

        topic.Name = request.Dto.Name;
        topic.Description = request.Dto.Description;
        topic.Order = request.Dto.Order;

        await _uow.Topics.UpdateAsync(topic, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Konu güncellendi");
    }
}