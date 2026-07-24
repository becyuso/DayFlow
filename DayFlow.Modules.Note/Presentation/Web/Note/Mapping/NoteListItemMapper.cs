using DayFlow.Modules.Notes.Application.Features.Note.List;
using DayFlow.Modules.Notes.Presentation.Web.Note.ViewModels;

namespace DayFlow.Modules.Notes.Presentation.Web.Note.Mapping
{
    public static class NoteListItemMapper
    {
        public static NoteListItemViewModel ToViewModel(
            this ListResult result)
        {
            return new NoteListItemViewModel
            {
                NoteId = result.NoteId,
                NotebookId = result.NotebookId,
                Title = result.Title,
                Summary = result.Summary,
                UpdatedAt = result.UpdatedAt
            };
        }
    }
}