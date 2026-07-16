using DayFlow.Modules.Notes.Application.Common;
using DayFlow.Modules.Notes.Application.Common.Interfaces;
using DayFlow.Modules.Notes.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Modules.Notes.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly NoteDbContext _db;

        public TransactionBehavior(
            NoteDbContext db)
        {
            _db = db;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var strategy =
                _db.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(
                async () =>
                {
                    await using var transaction =
                        await _db.Database.BeginTransactionAsync(
                            cancellationToken);
                    try
                    {
                        var response =
                            await next();

                        /*
                         *
                         * UnitOfWork SaveChanges
                         *
                         * 這裡統一 Commit 前寫入 DB
                         *
                         */

                        await _db.SaveChangesAsync(
                            cancellationToken);

                        await transaction.CommitAsync(
                            cancellationToken);

                        return response;
                    }
                    catch (DbUpdateException ex)
                    {

                        await transaction.RollbackAsync(
                            cancellationToken);

                        if (typeof(IResult).IsAssignableFrom(typeof(TResponse)))
                        {
                            dynamic resultType = typeof(TResponse);

                            return (TResponse)resultType
                                .GetMethod(nameof(Result.Message))!
                                .Invoke(null, new object[] { ex })!;
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
}