using System;

namespace DayFlow.Modules.Note.Domain.Entities
{
    // Represents notes.notebooks
    public class Notebook
    {
        public Guid NotebookId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Color { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        // Navigation
        public ICollection<Notes>? Notes { get; set; }

        // EF 用的 protected ctor
        protected Notebook() { }
    }
}
