namespace DayFlow.BuildingBlocks.Application.Abstractions
{
    public interface ITransactionManager
    {
        Task<TResponse> ExecuteAsync<TResponse>(
            Func<Task<TResponse>> action,
            CancellationToken cancellationToken = default);
    }
}
