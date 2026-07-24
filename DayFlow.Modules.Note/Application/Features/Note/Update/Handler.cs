using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.BuildingBlocks.Infrastructure.Time;
using DayFlow.Modules.Notes.Domain.Notes;
using MediatR;

namespace DayFlow.Modules.Notes.Application.Features.Note.Update
{
    public class Handler
        : IRequestHandler<UpdateCommand, Result<UpdateResult>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IClock _iClock;

        public Handler(INoteRepository noteRepository, IClock iClock) =>
            (_noteRepository, _iClock) = (noteRepository, iClock);

        public async Task<Result<UpdateResult>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _noteRepository.GetByIdAsync(request.NoteId, cancellationToken);

            if (entity == null)
                return Result<UpdateResult>.Fail("Note not found");

            // optional: enforce owner
            if (entity.UserId != request.UserId && request.UserId != Guid.Empty)
                return Result<UpdateResult>.Fail("Not authorized to update this note");

            entity.Update(
                request.Title,
                request.Content,
                request.Summary,
                _iClock.TaiwanNow,
                request.UserId);

            return Result<UpdateResult>.Ok(new());
        }
    }
}
