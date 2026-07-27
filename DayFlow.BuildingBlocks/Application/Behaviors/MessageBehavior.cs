using DayFlow.BuildingBlocks.Application.Messages;
using DayFlow.BuildingBlocks.Application.Results;
using MediatR;

namespace DayFlow.BuildingBlocks.Application.Behaviors;

public sealed class MessageBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IMessageCatalog _catalog;


    public MessageBehavior(
        IMessageCatalog catalog)
    {
        _catalog = catalog;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response =
            await next();


        if (response is Result result)
        {
            ApplyMessage(result);
        }

        return response;
    }

    private void ApplyMessage(
        Result result)
    {
        if (string.IsNullOrWhiteSpace(result.Code))
        {
            return;
        }

        result.SetMessage(
            _catalog.Get(result.Code));
    }
}