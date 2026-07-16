using System;

namespace DayFlow.Modules.Notes.Domain.Entities
{
    // Represents notes.tags
    public class Tag
    {
        public Guid TagId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Color { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        // Navigation
        public ICollection<NoteTag>? NoteTags { get; set; }


        // EF 用的 protected ctor
        protected Tag() { }
    }
}
