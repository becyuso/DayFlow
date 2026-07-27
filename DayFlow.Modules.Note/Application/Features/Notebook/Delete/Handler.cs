using DayFlow.BuildingBlocks.Application.Messages;
using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.BuildingBlocks.Infrastructure.Time;
using DayFlow.Modules.Notes.Application.Messages;
using DayFlow.Modules.Notes.Domain.Notebooks;
using DayFlow.Modules.Notes.Domain.Notes;
using MediatR;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Delete
{
    internal class Handler : IRequestHandler<DeleteCommand, Result<DeleteResult>>
    {
        private readonly INotebookRepository _notebookRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IClock _iClock;

        public Handler(INotebookRepository notebookRepository, INoteRepository noteRepository, IClock iClock) =>
            (_notebookRepository, _noteRepository, _iClock) = (notebookRepository, noteRepository, iClock);

        public async Task<Result<DeleteResult>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _notebookRepository.GetByIdAsync(request.NotebookId, cancellationToken);

            if (entity == null)
                return Result<DeleteResult>.Fail(NoteMessageCodes.NotebookNotFound);

            // optional: enforce owner
            if (entity.UserId != request.UserId && request.UserId != Guid.Empty)
                return Result<DeleteResult>.Fail(CommonMessageCode.Unauthorized);

            entity.SoftDelete(_iClock.TaiwanNow, request.UserId);

            await _noteRepository.SoftDeleteByNotebookIdAsync(entity.NotebookId, _iClock.TaiwanNow, cancellationToken);

            return Result<DeleteResult>.Ok(
                CommonMessageCode.DeleteSuccess, 
                new());
        }
    }
}
