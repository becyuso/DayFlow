using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Paging;
using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.Modules.Notes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.List;

public sealed class Handler
    : IQuery<PagedResult<QueryResult>>
{
    private readonly NoteDbContext _db;

    public Handler(NoteDbContext db)
    {
        _db = db;
    }

    public async Task<Result<PagedResult<QueryResult>>> Handle(
        Query request,
        CancellationToken cancellationToken)
    {
        var query = _db.Notebooks
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId);

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            query = query.Where(x =>
                x.Name.Contains(request.Keyword));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .Skip(request.Paging.Skip)
            .Take(request.Paging.Take)
            .Select(x => new QueryResult
            {
                NotebookId = x.NotebookId,
                Name = x.Name,
                Color = x.Color,
                SortOrder = x.SortOrder,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<QueryResult>
        {
            Items = items,
            Page = request.Paging.Page,
            PageSize = request.Paging.PageSize,
            TotalCount = totalCount
        };

        return Result<PagedResult<QueryResult>>.Ok(result);
    }
}