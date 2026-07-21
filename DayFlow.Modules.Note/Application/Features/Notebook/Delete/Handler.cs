using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.BuildingBlocks.Infrastructure.Time;
using DayFlow.Modules.Notes.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Delete
{
    internal class Handler : IRequestHandler<DeleteCommand, Result<DeleteResult>>
    {
        private readonly NoteDbContext _db;
        private readonly IClock _iClock;

        public Handler(NoteDbContext db, IClock iClock) =>
            (_db, _iClock) = (db, iClock);

        public async Task<Result<DeleteResult>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _db.Notebooks.FirstOrDefaultAsync(x => x.NotebookId == request.NotebookId && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result<DeleteResult>.Fail("Notebook not found");

            // optional: enforce owner
            if (entity.UserId != request.UserId && request.UserId != System.Guid.Empty)
                return Result<DeleteResult>.Fail("Not authorized to delete this notebook");

            entity.IsDeleted = true;
            entity.DeletedAt = _iClock.TaiwanNow;
            entity.DeletedBy = request.UserId == System.Guid.Empty ? null : request.UserId;

            await _db.SaveChangesAsync(cancellationToken);

            return Result<DeleteResult>.Ok(new());
        }
    }
}
