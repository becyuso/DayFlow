using DayFlow.Web.UI.Components.Pagination;

namespace DayFlow.Modules.Notes.Presentation.Web.Note.ViewModels;

public sealed class NoteListViewModel
{
    public Guid NotebookId { get; init; }

    public string NotebookName { get; init; } = "";

    public IReadOnlyList<NoteListItemViewModel> Items { get; init; } = [];

    public PaginationViewModel Pagination { get; init; } = new();
}