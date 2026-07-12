namespace DayFlow.Modules.Note.Domain.Entities
{
    // Represents notes.note_tags
    public class NoteTag
    {
        public Guid NoteId { get; set; }
        public Guid TagId { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }

        // Navigation (optional)
        public Notes? Notes { get; set; }
        public Tag? Tag { get; set; }

        // EF 用的 protected ctor
        protected NoteTag() { }
    }
}
