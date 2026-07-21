using DayFlow.BuildingBlocks.Application.Paging;
using DayFlow.Modules.Notes.Application.Features.Notebook.List;
using DayFlow.Modules.Notes.Presentation.Web.Notebook.ViewModels;
using DayFlow.Web.UI.Components.Pagination;

namespace DayFlow.Modules.Notes.Presentation.Web.Notebook.Mapping
{
    public static class NotebookListMapper
    {
        public static NotebookListViewModel ToViewModel(
         this PagedResult<ListResult> result,
         string? keyword)
        {
            return new()
            {
                Items = result.Items
                    .Select(x => x.ToViewModel())
                    .ToList(),

                Pagination =
                result.ToPagination(
                    "Index",
                    new
                    {
                        keyword
                    })
            };
        }
    }
}