using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Results;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Create
{
    public sealed record CreateCommand(Guid? UserId, string? Name, string? Color, int? SortOrder)
        : ICommand<Result<CreateResult>>;

    public sealed record CreateResult
    {
        public Guid? NotebookId { get; init; }
    }
}
