using DayFlow.BuildingBlocks.Application.Abstractions;
using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.Modules.Notes.Infrastructure.Database;

namespace DayFlow.Modules.Notes.Infrastructure.Persistence;

public sealed class EfUnitOfWork
    : IUnitOfWork
{
    private readonly NoteDbContext _db;

    public EfUnitOfWork(
        NoteDbContext db)
    {
        _db = db;
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _db.SaveChangesAsync(
            cancellationToken);
    }
}