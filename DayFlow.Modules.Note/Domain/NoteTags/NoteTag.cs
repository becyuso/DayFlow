namespace DayFlow.Modules.Notes.Domain.NoteTags
{
    // Represents notes.note_tags
    public class NoteTag
    {
        public Guid NoteId { get; private set; }
        public Guid TagId { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public Guid CreatedBy { get; set; }

        // EF 用的 protected ctor
        protected NoteTag() { }
    }
}
