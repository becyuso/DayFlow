using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DayFlow.Modules.Notes.Presentation.Web.Note.ViewModels;

public class NoteEditViewModel
{
    public Guid? NoteId { get; set; }

    [Display(Name = "筆記本")]
    [Required]
    public Guid NotebookId { get; set; }

    [Display(Name = "標題")]
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = "";

    [Display(Name = "內容")]
    public string? Content { get; set; }

    [Display(Name = "摘要")]
    [StringLength(500)]
    public string? Summary { get; set; }

    public string NotebookName { get; init; } = "";

    public IEnumerable<SelectListItem> NotebookOptions { get; set; }
        = Enumerable.Empty<SelectListItem>();
}