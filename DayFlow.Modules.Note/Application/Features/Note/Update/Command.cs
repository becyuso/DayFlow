using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Results;

namespace DayFlow.Modules.Notes.Application.Features.Note.Update
{
    public sealed record UpdateCommand(
        Guid UserId,
        Guid NoteId,
        string Title,
        string? Content,
        string? Summary) : ICommand<Result<UpdateResult>>;

    public sealed record UpdateResult
    {
    }
}
