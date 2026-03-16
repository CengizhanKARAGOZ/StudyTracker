using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.DTOs.Topic;
using StudyTracker.Application.Features.Topics.Commands;
using StudyTracker.Application.Features.Topics.Queries;

namespace StudyTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TopicsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("by-subject/{subjectId:guid}")]
    public async Task<IActionResult> GetBySubject(Guid subjectId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetTopicsBySubjectQuery(subjectId), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTopicDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateTopicCommand(dto), ct);
        if (!result.Success) return BadRequest(result);
        return CreatedAtAction(nameof(GetBySubject), new { subjectId = dto.SubjectId }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTopicDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateTopicCommand(id, dto), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeleteTopicCommand(id), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
}