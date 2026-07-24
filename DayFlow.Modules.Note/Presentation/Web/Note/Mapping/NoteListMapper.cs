using DayFlow.BuildingBlocks.Application.Paging;
using DayFlow.Modules.Notes.Application.Features.Note.List;
using DayFlow.Modules.Notes.Presentation.Web.Note.ViewModels;
using DayFlow.Web.UI.Components.Pagination;

namespace DayFlow.Modules.Notes.Presentation.Web.Note.Mapping
{
    public static class NoteListMapper
    {
        public static NoteListViewModel ToViewModel(
            this PagedResult<ListResult> result,
            Guid notebookId,
            string notebookName)
        {
            return new NoteListViewModel
            {
                NotebookId = notebookId,
                NotebookName = notebookName,
                Items = [.. result.Items.Select(x => x.ToViewModel())],
                Pagination = result.ToPagination(
                    "Index",
                    new
                    {
                        notebookId,
                    })
            };
        }
    }
}