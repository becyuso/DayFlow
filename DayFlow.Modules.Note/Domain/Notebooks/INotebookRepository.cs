using MediatR;

namespace DayFlow.Modules.Notes.Domain.Notebooks
{
    public interface INotebookRepository
    {
        Task<Notebook?> GetByIdAsync(Guid notebookId, CancellationToken cancellationToken = default);

        Task<Notebook?> GetByIdReadOnlyAsync(Guid notebookId, CancellationToken cancellationToken = default);

        void Add(Notebook notebook);
    }
}
