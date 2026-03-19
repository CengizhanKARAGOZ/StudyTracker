using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Goal;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Goals.Queries;

public record GetActiveGoalsQuery : IRequest<ApiResponse<List<GoalDto>>>;

public class GetActiveGoalsHandler : IRequestHandler<GetActiveGoalsQuery, ApiResponse<List<GoalDto>>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetActiveGoalsHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GoalDto>>> Handle(GetActiveGoalsQuery request, CancellationToken ct)
    {
        var goals = await _uow.Goals.GetActiveGoalsAsync(ct);
        var dtos = _mapper.Map<List<GoalDto>>(goals);
        return ApiResponse<List<GoalDto>>.Ok(dtos);
    }
}