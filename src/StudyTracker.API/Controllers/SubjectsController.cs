using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.DTOs.Subject;
using StudyTracker.Application.Features.Subjects.Commands;
using StudyTracker.Application.Features.Subjects.Queries;

namespace StudyTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubjectsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllSubjectsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDetail(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetSubjectDetailQuery(id), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSubjectDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateSubjectCommand(dto), ct);
        return CreatedAtAction(nameof(GetDetail), new { id = result.Data?.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSubjectDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateSubjectCommand(id, dto), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeleteSubjectCommand(id), ct);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
}