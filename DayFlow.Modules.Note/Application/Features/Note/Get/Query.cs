using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Results;

namespace DayFlow.Modules.Notes.Application.Features.Note.Get
{
    public sealed record GetQuery(
        Guid UserId,
        Guid NoteId)
        : IQuery<Result<GetResult>>;


    public sealed record GetResult
    {
        public Guid NoteId { get; init; }

        public Guid NotebookId { get; init; }

        public string Title { get; init; } = string.Empty;

        public string? Content { get; init; }

        public string? Summary { get; init; }

        public DateTime CreatedAt { get; init; }
    }
}
