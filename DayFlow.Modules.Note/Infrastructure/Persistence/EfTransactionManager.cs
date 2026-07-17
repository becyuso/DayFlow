using DayFlow.BuildingBlocks.Application.Abstractions;
using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.Modules.Notes.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Infrastructure.Persistence;


public sealed class EfTransactionManager
    : ITransactionManager
{
    private readonly NoteDbContext _db;

    public EfTransactionManager(
        NoteDbContext db)
    {
        _db = db;
    }

    public async Task<TResponse> ExecuteAsync<TResponse>(
        Func<Task<TResponse>> action,
        CancellationToken cancellationToken = default)
    {
        var strategy =
            _db.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(
            async () =>
            {
                await using var transaction =
                    await _db.Database
                        .BeginTransactionAsync(
                            cancellationToken);

                try
                {
                    var result =
                        await action();

                    await transaction.CommitAsync(
                        cancellationToken);

                    return result;
                }
                catch (DbUpdateException ex)
                {

                    await transaction.RollbackAsync(
                        cancellationToken);

                    if (typeof(IResult).IsAssignableFrom(typeof(TResponse)))
                    {
                        dynamic resultType = typeof(TResponse);

                        return (TResponse)resultType
                            .GetMethod(nameof(Result.Fail))!
                            .Invoke(null, new object[] { ex.Message })!;
                    }

                    throw;

                    //throw new DatabaseException(
                    //    "Database update failed.",
                    //    ex);
                }
                catch
                {
                    await transaction.RollbackAsync(
                        cancellationToken);

                    throw;
                }

            });
    }
}