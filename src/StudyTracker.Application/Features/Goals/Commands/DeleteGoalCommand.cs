using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Goals.Commands;

public record DeleteGoalCommand(Guid Id) : IRequest<ApiResponse<bool>>;

public class DeleteGoalHandler : IRequestHandler<DeleteGoalCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public DeleteGoalHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(DeleteGoalCommand request, CancellationToken ct)
    {
        var goal = await _uow.Goals.GetByIdAsync(request.Id, ct);
        if (goal is null)
            return ApiResponse<bool>.Fail("Hedef bulunamadı");

        await _uow.Goals.DeleteAsync(goal, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Hedef silindi");
    }
}