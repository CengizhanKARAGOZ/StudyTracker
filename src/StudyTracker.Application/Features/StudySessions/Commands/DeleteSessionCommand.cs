using MediatR;
using StudyTracker.Application.Common;
using StudyTracker.Domain.Interfaces;

namespace StudyTracker.Application.Features.StudySessions.Commands;

public record DeleteSessionCommand(Guid Id) : IRequest<ApiResponse<bool>>;

public class DeleteSessionHandler : IRequestHandler<DeleteSessionCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _uow;

    public DeleteSessionHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<ApiResponse<bool>> Handle(DeleteSessionCommand request, CancellationToken ct)
    {
        var session = await _uow.StudySessions.GetByIdAsync(request.Id, ct);
        if (session is null)
            return ApiResponse<bool>.Fail("Oturum bulunamadı");

        await _uow.StudySessions.DeleteAsync(session, ct);
        await _uow.SaveChangesAsync(ct);

        return ApiResponse<bool>.Ok(true, "Oturum silindi");
    }
}