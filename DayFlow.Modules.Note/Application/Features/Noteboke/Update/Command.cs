using DayFlow.Modules.Notes.Application.Common;
using DayFlow.Modules.Notes.Application.Common.Interfaces;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Update
{
    public sealed record UpdateCommand(System.Guid NotebookId, System.Guid UserId, string Name, string? Color, int SortOrder)
        : ICommand<Result<UpdateResult>>;

    public sealed record UpdateResult
    {
    }
}
