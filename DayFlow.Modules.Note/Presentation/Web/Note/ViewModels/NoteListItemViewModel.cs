using System.ComponentModel.DataAnnotations;

namespace DayFlow.Modules.Notes.Presentation.Web.Note.ViewModels
{
    public sealed record NoteListItemViewModel
    {
        public Guid? NoteId { get; init; }

        public Guid? NotebookId { get; init; }

        [Display(Name = "標題")]
        public string? Title { get; init; }

        [Display(Name = "摘要")]
        public string? Summary { get; init; }

        [Display(Name = "修改時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime? UpdatedAt { get; init; }
    }
}
