using Microsoft.EntityFrameworkCore;
using DayFlow.Modules.Notes.Domain.Entities;
using DayFlow.Modules.Notes.Infrastructure.Database;

namespace DayFlow.Modules.Notes.Infrastructure.Repositories
{
    public interface ITagRepository
    {
        Task<Tag?> GetByTagIdAsync(Guid? tagId);
    }

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
