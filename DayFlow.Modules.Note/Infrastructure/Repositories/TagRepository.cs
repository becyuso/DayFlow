using Microsoft.EntityFrameworkCore;
using DayFlow.Modules.Note.Domain.Entities;
using DayFlow.Modules.Note.Infrastructure.Database;

namespace DayFlow.Modules.Note.Infrastructure.Repositories
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
