using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Results;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Get;

public sealed record GetQuery(
    Guid UserId,
    Guid NotebookId)
    : IQuery<Result<GetResult>>;


public sealed record GetResult
{
    public Guid NotebookId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? Color { get; init; }

    public int SortOrder { get; init; }

    public DateTime CreatedAt { get; init; }
}