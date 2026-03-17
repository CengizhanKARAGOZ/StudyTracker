using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.DTOs.StudySession;
using StudyTracker.Application.Features.StudySessions.Commands;
using StudyTracker.Application.Features.StudySessions.Queries;

namespace StudyTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudySessionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudySessionsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetSessions(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] Guid? topicId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(new GetSessionsQuery(from, to, topicId), ct);
        return Ok(result);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetActiveSessionQuery(), ct);
        return Ok(result);
    }

    [HttpPost("start")]
    public async Task<IActionResult> Start([FromBody] StartSessionDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new StartSessionCommand(dto), ct);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPut("{id:guid}/pause")]
    public async Task<IActionResult> Pause(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new PauseSessionCommand(id), ct);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPut("{id:guid}/resume")]
    public async Task<IActionResult> Resume(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new ResumeSessionCommand(id), ct);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPut("{id:guid}/stop")]
    public async Task<IActionResult> Stop(Guid id, [FromBody] StopSessionDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new StopSessionCommand(id, dto), ct);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("manual")]
    public async Task<IActionResult> Manual([FromBody] ManualSessionDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new ManualSessionCommand(dto), ct);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeleteSessionCommand(id), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
}