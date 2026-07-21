using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.Modules.Notes.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Application.Features.Notebook.Get;

public sealed class Handler
    : IRequestHandler<GetQuery, Result<GetResult>>
{
    private readonly NoteDbContext _db;

    public Handler(NoteDbContext db)
    {
        _db = db;
    }

    public async Task<Result<GetResult>> Handle(
        GetQuery request,
        CancellationToken cancellationToken)
    {
        var result =
            await _db.Notebooks
                .AsNoTracking()
                .Where(x =>
                    x.NotebookId == request.NotebookId &&
                    x.UserId == request.UserId)
                .Select(x => new GetResult
                {
                    NotebookId = x.NotebookId,
                    Name = x.Name,
                    Color = x.Color,
                    SortOrder = x.SortOrder,
                    CreatedAt = x.CreatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            return Result<GetResult>.Fail(
                "Notebook not found.");
        }

        return Result<GetResult>.Ok(result);
    }
}