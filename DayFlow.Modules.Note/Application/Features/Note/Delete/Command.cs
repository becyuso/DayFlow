using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Results;

namespace DayFlow.Modules.Notes.Application.Features.Note.Delete
{
    public sealed record DeleteCommand(Guid NoteId, Guid UserId)
        : ICommand<Result<DeleteResult>>;

    public sealed record DeleteResult
    {
    }
}
