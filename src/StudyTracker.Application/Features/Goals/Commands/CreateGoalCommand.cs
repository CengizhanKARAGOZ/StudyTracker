using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Goal;
using StudyTracker.Domain.Entities;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Goals.Commands;

public record CreateGoalCommand(CreateGoalDto Dto) : IRequest<ApiResponse<GoalDto>>;

public class CreateGoalHandler : IRequestHandler<CreateGoalCommand, ApiResponse<GoalDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateGoalHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<GoalDto>> Handle(CreateGoalCommand request, CancellationToken ct)
    {
        if (request.Dto.SubjectId.HasValue)
        {
            var subjectExists = await _uow.Subjects.ExistsAsync(request.Dto.SubjectId.Value, ct);
            if (!subjectExists)
                return ApiResponse<GoalDto>.Fail("Ders bulunamadı");
        }

        if (request.Dto.TargetMinutes <= 0)
            return ApiResponse<GoalDto>.Fail("Hedef süre 0'dan büyük olmalı");

        var goal = _mapper.Map<Goal>(request.Dto);

        // Streak oluştur
        goal.Streak = new Streak
        {
            GoalId = goal.Id,
            CurrentStreak = 0,
            LongestStreak = 0,
            LastActivityDate = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        await _uow.Goals.AddAsync(goal, ct);
        await _uow.SaveChangesAsync(ct);

        var dto = _mapper.Map<GoalDto>(goal);
        return ApiResponse<GoalDto>.Ok(dto, "Hedef oluşturuldu");
    }
}