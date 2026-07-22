using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Results;

namespace DayFlow.Modules.Notes.Application.Features.Note.Update
{
    public sealed record UpdateCommand(
       Guid NoteId,
       Guid UserId,
       string Title,
       string? Content) : ICommand<Result<UpdateCommand>>;

    public sealed record UpdateResult
    {
    }
}
