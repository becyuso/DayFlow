using System;
using System.ComponentModel.DataAnnotations;

namespace DayFlow.Modules.Notes.Presentation.Web.Notebook.ViewModels
{
    public class NotebookEditViewModel
    {
        public Guid? NotebookId { get; set; }

        [Display(Name = "筆記本名稱")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [Display(Name = "顏色")]
        [StringLength(50)]
        public string? Color { get; set; }

        [Display(Name = "排序順序")]
        public int SortOrder { get; set; }
    }
}
