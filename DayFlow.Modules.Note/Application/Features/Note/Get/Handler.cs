using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.Modules.Notes.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Application.Features.Note.Get;

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
            await _db.Notes
                .AsNoTracking()
                .Where(x =>
                    x.NoteId == request.NoteId &&
                    x.UserId == request.UserId)
                .Select(x => new GetResult
                {
                    NoteId = x.NoteId,
                    NotebookId = x.NotebookId,
                    Title = x.Title,
                    Content = x.Content,
                    Summary = x.Summary,
                    CreatedAt = x.CreatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            return Result<GetResult>.Fail(
                "Notenot found.");
        }

        return Result<GetResult>.Ok(result);
    }
}