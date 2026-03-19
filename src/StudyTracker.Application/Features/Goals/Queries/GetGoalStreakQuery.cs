using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Goal;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Goals.Queries;

public record GetGoalStreakQuery(Guid GoalId) : IRequest<ApiResponse<GoalDto>>;

public class GetGoalStreakHandler : IRequestHandler<GetGoalStreakQuery, ApiResponse<GoalDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetGoalStreakHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<GoalDto>> Handle(GetGoalStreakQuery request, CancellationToken ct)
    {
        var goals = await _uow.Goals.GetActiveGoalsAsync(ct);
        var goal = goals.FirstOrDefault(g => g.Id == request.GoalId);

        if (goal is null)
        {
            var inactive = await _uow.Goals.GetByIdAsync(request.GoalId, ct);
            if (inactive is null)
                return ApiResponse<GoalDto>.Fail("Hedef bulunamadı");

            var inactiveDto = _mapper.Map<GoalDto>(inactive);
            return ApiResponse<GoalDto>.Ok(inactiveDto);
        }

        var dto = _mapper.Map<GoalDto>(goal);
        return ApiResponse<GoalDto>.Ok(dto);
    }
}