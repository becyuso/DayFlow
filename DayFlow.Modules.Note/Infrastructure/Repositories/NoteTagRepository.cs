using Microsoft.EntityFrameworkCore;
using DayFlow.Modules.Note.Domain.Entities;
using DayFlow.Modules.Note.Infrastructure.Database;

namespace DayFlow.Modules.Note.Infrastructure.Repositories
{
    public interface INoteTagRepository
    {
        Task<NoteTag?> GetByNoteIdAsync(Guid? noteId, Guid? tagId);
    }

    public class NoteTagRepository : INoteTagRepository
    {
        private readonly NoteDbContext _db;

        public NoteTagRepository(NoteDbContext db)
        {
            _db = db;
        }

        public async Task<NoteTag?> GetByNoteIdAsync(Guid? noteId, Guid? tagId)
        {
            return await _db.NoteTags.FirstOrDefaultAsync(x => x.NoteId == noteId && x.TagId == tagId);
        }
    }
}
