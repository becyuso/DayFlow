using DayFlow.Modules.Notes.Domain.Notebooks;
using DayFlow.Modules.Notes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Infrastructure.Repositories
{
    public class NotebookRepository : INotebookRepository
    {
        private readonly NoteDbContext _db;

        public NotebookRepository(NoteDbContext db) => _db = db;

        public async Task<Notebook?> GetByIdAsync(Guid notebookId, CancellationToken cancellationToken = default)
        {
            return await _db.Notebooks
                .FirstOrDefaultAsync(x => x.NotebookId == notebookId, cancellationToken);
        }

        public async Task<Notebook?> GetByIdReadOnlyAsync(Guid notebookId, CancellationToken cancellationToken = default)
        {
            return await _db.Notebooks
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NotebookId == notebookId, cancellationToken);
        }

        public void Add(Notebook notebook)
        {
            _db.Notebooks.Add(notebook);
        }
    }
}
