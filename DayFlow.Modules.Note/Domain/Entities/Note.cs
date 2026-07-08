using System;

namespace DayFlow.Modules.Note.Domain.Entities
{
    // Represents notes.notes
    public class Notes
    {
        public Guid NoteId { get; set; }
        public Guid NotebookId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public string? Summary { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        // Navigation
        public Notebook? Notebook { get; set; }
        public ICollection<NoteTag>? NoteTags { get; set; }

        // EF 用的 protected ctor
        protected Notes() { }

    }
}
