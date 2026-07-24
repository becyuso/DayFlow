using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.BuildingBlocks.Infrastructure.Time;
using DayFlow.Modules.Notes.Domain.Notebooks;
using MediatR;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Update
{
    public class Handler 
        : IRequestHandler<UpdateCommand, Result<UpdateResult>>
    {
        private readonly INotebookRepository _notebookRepository;
        private readonly IClock _iClock;

        public Handler(INotebookRepository notebookRepository, IClock iClock) =>
            (_notebookRepository, _iClock) = (notebookRepository, iClock);

        public async Task<Result<UpdateResult>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _notebookRepository.GetByIdAsync(request.NotebookId, cancellationToken);

            if (entity == null)
                return Result<UpdateResult>.Fail("Notebook not found");

            // optional: enforce owner
            if (entity.UserId != request.UserId && request.UserId != Guid.Empty)
                return Result<UpdateResult>.Fail("Not authorized to update this notebook");

            entity.Update(
                request.Name,
                request.Color,
                request.SortOrder,
                _iClock.TaiwanNow,
                request.UserId);

            return Result<UpdateResult>.Ok(new());
        }
    }
}
