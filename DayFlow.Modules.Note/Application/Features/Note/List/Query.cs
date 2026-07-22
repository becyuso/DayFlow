using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Paging;
using DayFlow.BuildingBlocks.Application.Results;

namespace DayFlow.Modules.Notes.Application.Features.Note.List;

public sealed record ListQuery(
    Guid UserId,
    Guid NotebookId,
    string? Keyword,
    PagingRequest Paging)
    : IQuery<Result<PagedResult<ListResult>>>;

public sealed record ListResult
{
    public Guid NoteId { get; init; }

    public string Title { get; init; } = string.Empty;

    public string? Summary { get; init; }

    public DateTime? UpdatedAt { get; init; }
}
