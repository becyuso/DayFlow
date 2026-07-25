namespace DayFlow.Modules.Notes.Domain.Notes
{
    // Represents notes.notes
    public class Note
    {
        public Guid NoteId { get; private set; }
        public Guid NotebookId { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; } = null!;
        public string? Content { get; private set; }
        public string? Summary { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid CreatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Guid? UpdatedBy { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public Guid? DeletedBy { get; private set; }

        // EF 用的 protected ctor
        protected Note() { }

        public static Note Create(Guid noteId, Guid notebookId, Guid userId, string title, string? content, string? summary, DateTime createdAt, Guid createdBy, DateTime updatedAt, Guid updatedBy)
        {
            var n = new Note
            {
                NoteId = noteId,
                NotebookId = notebookId,
                UserId = userId,
                Title = title,
                Content = content,
                Summary = summary,
                CreatedAt = createdAt,
                CreatedBy = createdBy,
                UpdatedAt = updatedAt,
                UpdatedBy = updatedBy,

                IsDeleted = false
            };
            return n;
        }

        public void Update(
            string title,
            string? content,
            string? summary,
            DateTime updatedAt,
            Guid updatedBy)
        {
            EnsureNotDeleted();

            Title = title;
            Content = content;
            Summary = summary;

            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }


        public void Delete(
            DateTime deletedAt,
            Guid deletedBy)
        {
            if (IsDeleted)
                return; // 冪等:已刪除就不重複處理,也不視為錯誤

            IsDeleted = true;
            DeletedAt = deletedAt;
            DeletedBy = deletedBy;
        }

        private void EnsureNotDeleted()
        {
            if (IsDeleted)
                throw new Exception(NoteId.ToString());
        }
    }
}
