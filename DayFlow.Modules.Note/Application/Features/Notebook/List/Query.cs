using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Paging;
using DayFlow.BuildingBlocks.Application.Results;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.List;

public sealed record ListQuery(
    Guid UserId,
    string? Keyword,
    PagingRequest Paging)
    : IQuery<Result<PagedResult<ListResult>>>;

public sealed record ListResult
{
    public Guid NotebookId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? Color { get; init; }

    public int SortOrder { get; init; }

    public DateTime CreatedAt { get; init; }
}