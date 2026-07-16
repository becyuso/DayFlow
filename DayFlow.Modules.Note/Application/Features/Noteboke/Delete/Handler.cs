using DayFlow.Modules.Notes.Application.Common;
using DayFlow.Modules.Notes.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Delete
{
    internal class Handler : IRequestHandler<DeleteCommand, Result<DeleteResult>>
    {
        private readonly NoteDbContext _db;

        public Handler(NoteDbContext db) => _db = db;

        public async Task<Result<DeleteResult>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _db.Notebooks.FirstOrDefaultAsync(x => x.NotebookId == request.NotebookId && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result<DeleteResult>.Fail("Notebook not found");

            // optional: enforce owner
            if (entity.UserId != request.UserId && request.UserId != System.Guid.Empty)
                return Result<DeleteResult>.Fail("Not authorized to delete this notebook");

            entity.IsDeleted = true;
            entity.DeletedAt = System.DateTime.UtcNow;
            entity.DeletedBy = request.UserId == System.Guid.Empty ? null : request.UserId;

            await _db.SaveChangesAsync(cancellationToken);

            return Result<DeleteResult>.Ok(new());
        }
    }
}
