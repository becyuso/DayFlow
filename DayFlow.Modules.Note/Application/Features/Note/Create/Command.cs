using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Results;

namespace DayFlow.Modules.Notes.Application.Features.Note.Create
{
    public sealed record CreateCommand(
     Guid UserId,
     Guid NotebookId,
     string Title,
     string? Content)
     : ICommand<Result<CreateResult>>;

    public sealed record CreateResult
    {
        public Guid? NoteId { get; init; }
    }
}
