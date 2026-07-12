using Microsoft.EntityFrameworkCore;
using DayFlow.Modules.Note.Domain.Entities;
using DayFlow.Modules.Note.Infrastructure.Database;

namespace DayFlow.Modules.Note.Infrastructure.Repositories
{
    public interface INotebookRepository
    {
        Task<Notebook?> GetByNotebookIdAsync(Guid? notebookId);
    }

    public class NotebookRepository : INotebookRepository
    {
        private readonly NoteDbContext _db;

        public NotebookRepository(NoteDbContext db)
        {
            _db = db;
        }

        public async Task<Notebook?> GetByNotebookIdAsync(Guid? notebookId)
        {
            return await _db.Notebooks.FirstOrDefaultAsync(x => x.NotebookId == notebookId);
        }
    }
}
