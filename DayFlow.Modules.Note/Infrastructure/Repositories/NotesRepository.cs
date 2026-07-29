using DayFlow.Modules.Notes.Domain.Notes;
using DayFlow.Modules.Notes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteDbContext _db;

        public NoteRepository(NoteDbContext db) => _db = db;

        public async Task<Note?> GetByIdAsync(Guid? noteId, CancellationToken cancellationToken = default)
        {
            return await _db.Notes
                .FirstOrDefaultAsync(x => x.NoteId == noteId, cancellationToken);
        }

        public async Task<Note?> GetByIdReadOnlyAsync(Guid noteId, CancellationToken cancellationToken = default)
        {
            return await _db.Notes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NoteId == noteId, cancellationToken);
        }

        public async Task<int> CountByNotebookIdAsync(
            Guid notebookId,
            CancellationToken cancellationToken = default)
        {
            return await _db.Notes
                .AsNoTracking()
                .CountAsync(
                    x => x.NotebookId == notebookId,
                    cancellationToken);
        }

        public void Add(Note note)
        {
            _db.Notes.Add(note);
        }

        public void Remove(Note note)
        {
            _db.Notes.Remove(note);
        }

        public async Task SoftDeleteByNotebookIdAsync(
            Guid notebookId,
            DateTime deletedAt,
            CancellationToken cancellationToken = default)
        {
            await _db.Notes
                .Where(x =>
                    x.NotebookId == notebookId &&
                    x.IsDeleted == false)
                .ExecuteUpdateAsync(
                    setters => setters
                        .SetProperty(
                            x => x.DeletedAt, deletedAt),
                    cancellationToken);
        }

        public async Task DeleteByNotebookIdAsync(
            Guid notebookId, CancellationToken
            cancellationToken = default)
        {
            await _db.Notes
                .Where(x => x.NotebookId == notebookId)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
