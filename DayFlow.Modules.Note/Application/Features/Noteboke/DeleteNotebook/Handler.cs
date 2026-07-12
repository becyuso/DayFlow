using MediatR;
using DayFlow.Modules.Note.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Note.Application.Features.Noteboke.DeleteNotebook
{
    internal class Handler : IRequestHandler<DeleteNotebook.Command, DeleteNotebook.Result>
    {
        private readonly NoteDbContext _db;

        public Handler(NoteDbContext db) => _db = db;

        public async Task<DeleteNotebook.Result> Handle(DeleteNotebook.Command request, CancellationToken cancellationToken)
        {
            var entity = await _db.Notebooks.FirstOrDefaultAsync(x => x.NotebookId == request.NotebookId && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return DeleteNotebook.Result.Fail("Notebook not found");

            // optional: enforce owner
            if (entity.UserId != request.UserId && request.UserId != System.Guid.Empty)
                return DeleteNotebook.Result.Fail("Not authorized to delete this notebook");

            entity.IsDeleted = true;
            entity.DeletedAt = System.DateTime.UtcNow;
            entity.DeletedBy = request.UserId == System.Guid.Empty ? null : request.UserId;

            await _db.SaveChangesAsync(cancellationToken);

            return DeleteNotebook.Result.Ok();
        }
    }
}
