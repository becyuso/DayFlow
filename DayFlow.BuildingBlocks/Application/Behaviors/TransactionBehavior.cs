using DayFlow.BuildingBlocks.Application.Abstractions;
using DayFlow.BuildingBlocks.Application.Messaging;
using MediatR;

namespace DayFlow.BuildingBlocks.Application.Behaviors
{
    public sealed class TransactionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {

        private readonly ITransactionManager _transaction;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehavior(
            ITransactionManager transaction,
            IUnitOfWork unitOfWork)
        {
            _transaction = transaction;
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            return await _transaction.ExecuteAsync(
                async () =>
                {
                    var response =
                        await next();

                    await _unitOfWork.SaveChangesAsync(
                        cancellationToken);

                    return response;

                },
                cancellationToken);
        }
    }
}
