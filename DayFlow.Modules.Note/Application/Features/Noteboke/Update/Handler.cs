using DayFlow.Modules.Notes.Application.Common;
using DayFlow.Modules.Notes.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Update
{
    public class Handler : IRequestHandler<UpdateCommand, Result<UpdateResult>>
    {
        private readonly NoteDbContext _db;

        public Handler(NoteDbContext db) => _db = db;

        public async Task<Result<UpdateResult>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _db.Notebooks.FirstOrDefaultAsync(x => x.NotebookId == request.NotebookId && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result<UpdateResult>.Fail("Notebook not found");

            // optional: enforce owner
            if (entity.UserId != request.UserId && request.UserId != System.Guid.Empty)
                return Result<UpdateResult>.Fail("Not authorized to update this notebook");

            entity.Name = request.Name;
            entity.Color = request.Color;
            entity.SortOrder = request.SortOrder;
            entity.UpdatedAt = System.DateTime.UtcNow;
            entity.UpdatedBy = request.UserId == System.Guid.Empty ? null : request.UserId;

            await _db.SaveChangesAsync(cancellationToken);

            return Result<UpdateResult>.Ok(new());
        }
    }
}
