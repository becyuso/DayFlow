using System;
using System.ComponentModel.DataAnnotations;

namespace DayFlow.Modules.Note.Presentation.Web.Notebook.ViewModels
{
    public class NotebookEditViewModel
    {
        public Guid? NotebookId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string? Color { get; set; }

        public int SortOrder { get; set; }
    }
}
