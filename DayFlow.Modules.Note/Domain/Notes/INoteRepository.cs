namespace DayFlow.Modules.Notes.Domain.Notes
{
    public interface INoteRepository
    {
        Task<Note?> GetByIdAsync(Guid? noteId, CancellationToken cancellationToken = default);

        Task<Note?> GetByIdReadOnlyAsync(Guid noteId, CancellationToken cancellationToken = default);

        void Add(Note note);

        void Remove(Note note);

        Task SoftDeleteByNotebookIdAsync(Guid notebookId, DateTime deletedAt, CancellationToken cancellationToken = default);

        Task DeleteByNotebookIdAsync(Guid notebookId, CancellationToken cancellationToken = default);
    }
}
