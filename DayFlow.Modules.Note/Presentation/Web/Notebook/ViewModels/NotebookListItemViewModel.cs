namespace DayFlow.Modules.Notes.Presentation.Web.Notebook.ViewModels
{
    public sealed record NotebookListItemViewModel
    {
        public Guid NotebookId { get; init; }

        public string Name { get; init; } = "";

        public string? Color { get; init; }

        public int SortOrder { get; init; }

        public DateTime CreatedAt { get; init; }
    }
}
