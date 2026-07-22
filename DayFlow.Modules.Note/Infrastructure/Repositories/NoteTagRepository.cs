using DayFlow.Modules.Notes.Domain.NoteTags;
using DayFlow.Modules.Notes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Infrastructure.Repositories
{
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
