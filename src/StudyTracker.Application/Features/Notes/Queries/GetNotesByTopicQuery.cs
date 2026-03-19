using AutoMapper;
using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Application.DTOs.Note;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.Notes.Queries;

public record GetNotesByTopicQuery(Guid TopicId) : IRequest<ApiResponse<List<NoteDto>>>;

public class GetNotesByTopicHandler : IRequestHandler<GetNotesByTopicQuery, ApiResponse<List<NoteDto>>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetNotesByTopicHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<NoteDto>>> Handle(GetNotesByTopicQuery request, CancellationToken ct)
    {
        var notes = await _uow.Notes.GetByTopicAsync(request.TopicId, ct);
        var dtos = _mapper.Map<List<NoteDto>>(notes);
        return ApiResponse<List<NoteDto>>.Ok(dtos);
    }
}