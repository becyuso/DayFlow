using DayFlow.BuildingBlocks.Domain.Exceptions;

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

        #region  Constructors
        // EF 用的 protected ctor
        protected Note() { }
        #endregion

        #region Factory Methods / Domain Behaviors
        public static Note Create(
            Guid noteId,
            Guid notebookId,
            Guid userId,
            string title,
            string? content,
            string? summary,
            DateTime createdAt,
            Guid createdBy,
            DateTime updatedAt,
            Guid updatedBy)
        {

            ValidateId(noteId);

            ValidateNotebookId(notebookId);

            ValidateUserId(userId);

            ValidateTitle(title);

            ValidateSummary(summary);

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

            ValidateTitle(title);

            ValidateSummary(summary);

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
        #endregion

        #region Guard
        private void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new DomainException(
                    "Note has already been deleted.");
            }
        }

        #endregion

        #region Validation

        private static void ValidateId(Guid noteId)
        {
            if (noteId == Guid.Empty)
            {
                throw new DomainException(
                    "NoteId is required.");
            }
        }

        private static void ValidateNotebookId(Guid notebookId)
        {
            if (notebookId == Guid.Empty)
            {
                throw new DomainException(
                    "NotebookId is required.");
            }
        }

        private static void ValidateUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new DomainException(
                    "UserId is required.");
            }
        }

        private static void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new DomainException(
                    "Note title is required.");
            }


            if (title.Length > 200)
            {
                throw new DomainException(
                    "Note title cannot exceed 200 characters.");
            }
        }

        private static void ValidateSummary(string? summary)
        {
            if (summary == null)
                return;


            if (summary.Length > 500)
            {
                throw new DomainException(
                    "Summary cannot exceed 500 characters.");
            }
        }

        #endregion
    }
}
