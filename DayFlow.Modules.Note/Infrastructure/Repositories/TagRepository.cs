using DayFlow.Modules.Notes.Domain.Tags;
using DayFlow.Modules.Notes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly NoteDbContext _db;

        public TagRepository(NoteDbContext db)
        {
            _db = db;
        }

        public async Task<Tag?> GetByTagIdAsync(Guid? tagId)
        {
            return await _db.Tags.FirstOrDefaultAsync(x => x.TagId == tagId);
        }
    }
}
