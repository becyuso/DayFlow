using DayFlow.Modules.Note.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Note.Application.Features.Noteboke.UpdateNotebook
{
    internal class Handler : IRequestHandler<UpdateNotebook.Command, UpdateNotebook.Result>
    {
        private readonly NoteDbContext _db;

        public Handler(NoteDbContext db) => _db = db;

        public async Task<UpdateNotebook.Result> Handle(UpdateNotebook.Command request, CancellationToken cancellationToken)
        {
            var entity = await _db.Notebooks.FirstOrDefaultAsync(x => x.NotebookId == request.NotebookId && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return UpdateNotebook.Result.Fail("Notebook not found");

            // optional: enforce owner
            if (entity.UserId != request.UserId && request.UserId != System.Guid.Empty)
                return UpdateNotebook.Result.Fail("Not authorized to update this notebook");

            entity.Name = request.Name;
            entity.Color = request.Color;
            entity.SortOrder = request.SortOrder;
            entity.UpdatedAt = System.DateTime.UtcNow;
            entity.UpdatedBy = request.UserId == System.Guid.Empty ? null : request.UserId;

            await _db.SaveChangesAsync(cancellationToken);

            return UpdateNotebook.Result.Ok();
        }
    }
}
