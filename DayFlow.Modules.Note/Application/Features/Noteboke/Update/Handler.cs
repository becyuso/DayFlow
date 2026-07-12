using DayFlow.Modules.Note.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Note.Application.Features.Notebook.Update
{
    internal class Handler : IRequestHandler<UpdateCommand, UpdateResult>
    {
        private readonly NoteDbContext _db;

        public Handler(NoteDbContext db) => _db = db;

        public async Task<UpdateResult> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _db.Notebooks.FirstOrDefaultAsync(x => x.NotebookId == request.NotebookId && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return UpdateResult.Fail("Notebook not found");

            // optional: enforce owner
            if (entity.UserId != request.UserId && request.UserId != System.Guid.Empty)
                return UpdateResult.Fail("Not authorized to update this notebook");

            entity.Name = request.Name;
            entity.Color = request.Color;
            entity.SortOrder = request.SortOrder;
            entity.UpdatedAt = System.DateTime.UtcNow;
            entity.UpdatedBy = request.UserId == System.Guid.Empty ? null : request.UserId;

            await _db.SaveChangesAsync(cancellationToken);

            return UpdateResult.Ok();
        }
    }
}
