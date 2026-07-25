namespace DayFlow.Modules.Notes.Domain.Notebooks
{
    // Represents notes.notebooks
    public class Notebook
    {
        public Guid NotebookId { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Color { get; private set; }
        public int SortOrder { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid CreatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Guid? UpdatedBy { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public Guid? DeletedBy { get; private set; }

        #region Navigation

        // 同屬一個Aggregate
        // Navigation:集合類型,必須有 backing field,
        // 外部只能透過 IReadOnlyCollection 讀取,不能直接 Add/Remove
        //private readonly List<Note> _notes = new();
        //public IReadOnlyCollection<Note> Notes => _notes;

        #endregion

        // EF 用的 protected ctor
        protected Notebook() { }

        // Public factory-like ctor for application code
        public static Notebook Create(Guid notebookId, Guid userId, string name, string? color, int sortOrder, DateTime createdAt, Guid createdBy, DateTime updatedAt, Guid updatedBy)
        {
            var n = new Notebook
            {
                NotebookId = notebookId,
                UserId = userId,
                Name = name,
                Color = color,
                SortOrder = sortOrder,
                CreatedAt = createdAt,
                CreatedBy = createdBy,
                UpdatedAt = updatedAt,
                UpdatedBy = updatedBy,

                IsDeleted = false
            };
            return n;
        }

        public void Update(
            string name,
            string? color,
            int sortOrder,
            DateTime updatedAt,
            Guid updatedBy)
        {
            EnsureNotDeleted();

            Name = name;
            Color = color;
            SortOrder = sortOrder;

            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
        }

        public void SoftDelete(
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
                throw new Exception(NotebookId.ToString());
        }
    }
}
