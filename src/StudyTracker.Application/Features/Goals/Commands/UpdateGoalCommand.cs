using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Goal;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Goals.Commands;

public record UpdateGoalCommand(Guid Id, UpdateGoalDto Dto) : IRequest<ApiResponse<bool>>;

public class UpdateGoalHandler : IRequestHandler<UpdateGoalCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public UpdateGoalHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(UpdateGoalCommand request, CancellationToken ct)
    {
        var goal = await _uow.Goals.GetByIdAsync(request.Id, ct);
        if (goal is null)
            return ApiResponse<bool>.Fail("Hedef bulunamadı");

        goal.Title = request.Dto.Title;
        goal.TargetMinutes = request.Dto.TargetMinutes;
        goal.GoalType = request.Dto.GoalType;
        goal.IsActive = request.Dto.IsActive;

        await _uow.Goals.UpdateAsync(goal, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Hedef güncellendi");
    }
}