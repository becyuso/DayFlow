using DayFlow.Modules.Notes.Domain.Notebooks;
using DayFlow.Modules.Notes.Domain.NoteTags;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Xml.Linq;

namespace DayFlow.Modules.Notes.Domain.Notes
{
    // Represents notes.notes
    public class Note
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
        protected Note() { }

        public static Note Create(Guid noteId, Guid notebookId, Guid userId, string title, string? content, DateTime createdAt, Guid createdBy, DateTime updatedAt, Guid updatedBy)
        {
            var n = new Note
            {
                NoteId = noteId,
                NotebookId = notebookId,
                UserId = userId,
                Title = title,
                Content = content,
                CreatedAt = createdAt,
                CreatedBy = createdBy,
                UpdatedAt = updatedAt,
                UpdatedBy = updatedBy,

                IsDeleted = false
            };
            return n;
        }

        public void Delete(
            DateTime deletedAt,
            Guid deletedBy)
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            DeletedAt = deletedAt;
            DeletedBy = deletedBy;
        }
    }
}
