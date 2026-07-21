using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Results;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Delete
{
    public sealed record DeleteCommand(System.Guid NotebookId, System.Guid UserId) 
        : ICommand<Result<DeleteResult>>;

    public sealed record DeleteResult
    {
    }
}
