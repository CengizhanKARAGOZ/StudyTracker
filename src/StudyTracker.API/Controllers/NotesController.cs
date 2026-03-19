using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.DTOs.Note;
using StudyTracker.Application.Features.Notes.Commands;
using StudyTracker.Application.Features.Notes.Queries;

namespace StudyTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotesController(IMediator mediator) => _mediator = mediator;

    [HttpGet("by-topic/{topicId:guid}")]
    public async Task<IActionResult> GetByTopic(Guid topicId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetNotesByTopicQuery(topicId), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateNoteCommand(dto), ct);
        if (!result.Success) return BadRequest(result);
        return CreatedAtAction(nameof(GetByTopic), new { topicId = dto.TopicId }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateNoteCommand(id, dto), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeleteNoteCommand(id), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPut("{id:guid}/pin")]
    public async Task<IActionResult> TogglePin(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new TogglePinNoteCommand(id), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
}