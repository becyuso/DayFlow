using Microsoft.EntityFrameworkCore;
using DayFlow.Modules.Notes.Domain.Entities;
using DayFlow.Modules.Notes.Infrastructure.Database;

namespace DayFlow.Modules.Notes.Infrastructure.Repositories
{
    public interface INoteRepository
    {
        Task<Note?> GetByNoteIdAsync(Guid? noteId);
    }

    public class NoteRepository : INoteRepository
    {
        private readonly NoteDbContext _db;

        public NoteRepository(NoteDbContext db)
        {
            _db = db;
        }

        public async Task<Note?> GetByNoteIdAsync(Guid? noteId)
        {
            return await _db.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId);
        }
    }
}
