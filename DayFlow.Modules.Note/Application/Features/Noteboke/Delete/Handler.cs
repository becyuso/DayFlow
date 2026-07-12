using MediatR;
using DayFlow.Modules.Note.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Note.Application.Features.Notebook.Delete
{
    internal class Handler : IRequestHandler<DeleteCommand, DeleteResult>
    {
        private readonly NoteDbContext _db;

        public Handler(NoteDbContext db) => _db = db;

        public async Task<DeleteResult> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _db.Notebooks.FirstOrDefaultAsync(x => x.NotebookId == request.NotebookId && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return DeleteResult.Fail("Notebook not found");

            // optional: enforce owner
            if (entity.UserId != request.UserId && request.UserId != System.Guid.Empty)
                return DeleteResult.Fail("Not authorized to delete this notebook");

            entity.IsDeleted = true;
            entity.DeletedAt = System.DateTime.UtcNow;
            entity.DeletedBy = request.UserId == System.Guid.Empty ? null : request.UserId;

            await _db.SaveChangesAsync(cancellationToken);

            return DeleteResult.Ok();
        }
    }
}
