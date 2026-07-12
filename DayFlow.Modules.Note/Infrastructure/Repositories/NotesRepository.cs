using Microsoft.EntityFrameworkCore;
using DayFlow.Modules.Note.Domain.Entities;
using DayFlow.Modules.Note.Infrastructure.Database;

namespace DayFlow.Modules.Note.Infrastructure.Repositories
{
    public interface INoteRepository
    {
        Task<Notes?> GetByNoteIdAsync(Guid? noteId);
    }

    public class NoteRepository : INoteRepository
    {
        private readonly NoteDbContext _db;

        public NoteRepository(NoteDbContext db)
        {
            _db = db;
        }

        public async Task<Notes?> GetByNoteIdAsync(Guid? noteId)
        {
            return await _db.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId);
        }
    }
}
