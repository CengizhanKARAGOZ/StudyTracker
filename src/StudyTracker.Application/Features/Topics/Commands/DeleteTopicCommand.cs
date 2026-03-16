using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Topics.Commands;

public record DeleteTopicCommand(Guid Id) : IRequest<ApiResponse<bool>>;

public class DeleteTopicHandler : IRequestHandler<DeleteTopicCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public DeleteTopicHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(DeleteTopicCommand request, CancellationToken ct)
    {
        var topic = await _uow.Topics.GetByIdAsync(request.Id, ct);
        if (topic is null)
            return ApiResponse<bool>.Fail("Konu bulunamadı");

        await _uow.Topics.DeleteAsync(topic, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Konu silindi");
    }
}