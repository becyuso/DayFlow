using DayFlow.Modules.Notes.Application.Features.Notebook.List;
using DayFlow.Modules.Notes.Presentation.Web.Notebook.ViewModels;

namespace DayFlow.Modules.Notes.Presentation.Web.Notebook.Mapping
{
    public static class NotebookListItemMapper
    {
        public static NotebookListItemViewModel ToViewModel(
            this ListResult result)
        {
            return new()
            {
                NotebookId = result.NotebookId,
                Name = result.Name,
                Color = result.Color,
                SortOrder = result.SortOrder,
                
                UpdatedAt= result.UpdatedAt
            };
        }
    }
}
