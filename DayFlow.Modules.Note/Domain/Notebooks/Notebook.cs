using DayFlow.BuildingBlocks.Domain.Exceptions;

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

        //private readonly List<NotebookShare> _shares = new();

        //public IReadOnlyCollection<NotebookShare> Shares
        //    => _shares.AsReadOnly();

        #endregion

        #region  Constructors
        // EF 用的 protected ctor
        protected Notebook() { }
        #endregion

        #region Domain Behavior

        //public void AddShare(
        //    Guid userId,
        //    NotebookShareRole role)
        //{
        //    if (_shares.Any(x => x.UserId == userId))
        //        throw new DomainException(
        //            "User already exists.");

        //    _shares.Add(
        //        new NotebookShare(
        //            Guid.NewGuid(),
        //            NotebookId,
        //            userId,
        //            role));
        //}

        //public void RemoveShare(
        //    Guid userId)
        //{
        //    var share =
        //        _shares.FirstOrDefault(
        //            x => x.UserId == userId);

        //    if (share == null)
        //        return;

        //    if (share.Role == NotebookShareRole.Owner &&
        //        _shares.Count(x => x.Role == NotebookShareRole.Owner) == 1)
        //    {
        //        throw new DomainException(
        //            "Notebook must have at least one owner.");
        //    }

        //    _shares.Remove(share);
        //}

        #endregion

        #region Factory Methods / Domain Behaviors
        public static Notebook Create(
            Guid notebookId,
            Guid userId,
            string name,
            string? color,
            int sortOrder,
            DateTime createdAt,
            Guid createdBy,
            DateTime updatedAt,
            Guid updatedBy)
        {
            ValidateId(notebookId);

            ValidateUserId(userId);

            ValidateName(name);

            ValidateSortOrder(sortOrder);

            ValidateColor(color);

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

            ValidateName(name);

            ValidateSortOrder(sortOrder);

            ValidateColor(color);

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

        #endregion

        #region Guard

        private void EnsureNotDeleted()
        {
            if (IsDeleted)
                throw new DomainException(
                    "Notebook has been deleted.");
        }

        #endregion

        #region Validation

        private static void ValidateId(Guid id)
        {
            if (id == Guid.Empty)
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

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(
                    "Notebook name is required.");
            }


            if (name.Length > 100)
            {
                throw new DomainException(
                    "Notebook name cannot exceed 100 characters.");
            }
        }

        private static void ValidateSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
            {
                throw new DomainException(
                    "SortOrder must be greater than or equal to 0.");
            }
        }

        private static void ValidateColor(string? color)
        {
            if (string.IsNullOrWhiteSpace(color))
                return;

            if (color.Length > 50)
            {
                throw new DomainException(
                    "Color cannot exceed 50 characters.");
            }


            // 如果使用 Hex Color
            // 例如 #FFFFFF
            if (!color.StartsWith("#"))
            {
                throw new DomainException(
                    "Color format is invalid.");
            }
        }

        #endregion

        #region Domain Rules

        /// <summary>
        /// 單一 Notebook 允許容納的最大 Note 筆數(不含已刪除的 Note)。
        /// </summary>
        public const int MaxNoteCount = 50;

        /// <summary>
        /// 判斷目前筆數是否仍允許再新增一筆 Note。
        /// </summary>
        /// <param name="currentNoteCount">
        /// 該 Notebook 目前的有效 Note 筆數(呼叫端應排除已刪除的 Note)。
        /// </param>
        public static bool EnsureCanAddNote(int currentNoteCount)
            => currentNoteCount < MaxNoteCount;

        /// <summary>
        /// 判斷是否已達上限(CanAddNote 的相反語意,方便在需要"已達上限"語意的地方直接使用,
        /// 避免呼叫端寫成 !CanAddNote(...) 這種否定表達,降低誤讀機率)。
        /// </summary>
        public static bool EnsureHasReachedLimit(int currentNoteCount)
            => !EnsureCanAddNote(currentNoteCount);

        #endregion
    }
}
