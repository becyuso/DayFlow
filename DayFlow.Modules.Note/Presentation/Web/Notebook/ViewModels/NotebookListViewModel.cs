using DayFlow.Web.UI.Components.Pagination;

namespace DayFlow.Modules.Notes.Presentation.Web.Notebook.ViewModels
{
    public sealed class NotebookListViewModel
    {
        public IReadOnlyList<NotebookListItemViewModel> Items { get; init; } = [];

        public PaginationViewModel Pagination { get; init; } = new();
    }
}
