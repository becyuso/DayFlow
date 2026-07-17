using MediatR;

namespace DayFlow.BuildingBlocks.Application.Messaging
{
    public interface IQuery<out TResponse>
        : IRequest<TResponse>
    {
    }
}
