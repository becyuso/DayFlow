using System.ComponentModel.DataAnnotations;

namespace DayFlow.Modules.Notes.Presentation.Web.Notebook.ViewModels
{
    public sealed record NotebookListItemViewModel
    {
        public Guid NotebookId { get; init; }

        [Display(Name = "名稱")]
        public string Name { get; init; } = "";

        [Display(Name = "顏色")]
        public string? Color { get; init; }

        [Display(Name = "排序")]
        public int SortOrder { get; init; }

        [Display(Name = "修改時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime? UpdatedAt { get; init; }
    }
}
