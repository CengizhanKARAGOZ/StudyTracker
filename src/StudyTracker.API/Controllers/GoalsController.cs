using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.DTOs.Goal;
using StudyTracker.Application.Features.Goals.Commands;
using StudyTracker.Application.Features.Goals.Queries;

namespace StudyTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoalsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetActive(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetActiveGoalsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}/streak")]
    public async Task<IActionResult> GetStreak(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetGoalStreakQuery(id), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGoalDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateGoalCommand(dto), ct);
        if (!result.Success) return BadRequest(result);
        return CreatedAtAction(nameof(GetStreak), new { id = result.Data?.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGoalDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateGoalCommand(id, dto), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeleteGoalCommand(id), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
}