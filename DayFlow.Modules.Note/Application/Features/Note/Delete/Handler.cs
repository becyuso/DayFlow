using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.BuildingBlocks.Infrastructure.Time;
using DayFlow.Modules.Notes.Domain.Notebooks;
using DayFlow.Modules.Notes.Domain.Notes;
using DayFlow.Modules.Notes.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Application.Features.Note.Delete
{
    internal class Handler : IRequestHandler<DeleteCommand, Result<DeleteResult>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IClock _iClock;

        public Handler(INoteRepository noteRepository, IClock iClock) =>
            (_noteRepository, _iClock) = (noteRepository, iClock);

        public async Task<Result<DeleteResult>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _noteRepository.GetByIdAsync(request.NoteId, cancellationToken);
            if (entity == null)
                return Result<DeleteResult>.Fail("Note not found");

            // optional: enforce owner
            if (entity.UserId != request.UserId && request.UserId != Guid.Empty)
                return Result<DeleteResult>.Fail("Not authorized to delete this note");

            entity.Delete(_iClock.TaiwanNow, request.UserId);

            return Result<DeleteResult>.Ok(new());
        }
    }
}
