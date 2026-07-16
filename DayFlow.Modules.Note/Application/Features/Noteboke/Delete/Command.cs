using DayFlow.Modules.Notes.Application.Common;
using DayFlow.Modules.Notes.Application.Common.Interfaces;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Delete
{
    public sealed record DeleteCommand(System.Guid NotebookId, System.Guid UserId) 
        : ICommand<Result<DeleteResult>>;

    public sealed record DeleteResult
    {
    }
}
