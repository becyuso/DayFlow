using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.BuildingBlocks.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse, TDbContext>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
        where TDbContext : DbContext
    {
        private readonly TDbContext _db;

        public TransactionBehavior(
            TDbContext db) => _db = db;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var strategy =
                _db.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction =
                    await _db.Database.BeginTransactionAsync(
                        cancellationToken);

                try
                {
                    var response =
                        await next();

                    // 只有成功才 Save
                    //if (response is IResult result &&
                    //    result.IsSuccess)
                    //{
                        await _db.SaveChangesAsync(
                            cancellationToken);
                    //}

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
}
