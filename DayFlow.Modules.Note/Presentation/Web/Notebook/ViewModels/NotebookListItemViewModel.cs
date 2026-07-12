using System;

namespace DayFlow.Modules.Note.Presentation.Web.Notebook.ViewModels
{
    public class NotebookListItemViewModel
    {
        public Guid NotebookId { get; set; }
        public string Name { get; set; } = null!;
        public string? Color { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
