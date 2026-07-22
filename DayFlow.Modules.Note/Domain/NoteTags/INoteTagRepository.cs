using DayFlow.Modules.Notes.Domain.Notes;

namespace DayFlow.Modules.Notes.Domain.NoteTags
{
    public interface INoteTagRepository
    {
        Task<NoteTag?> GetByNoteIdAsync(Guid? noteId, Guid? tagId);
    }
}
