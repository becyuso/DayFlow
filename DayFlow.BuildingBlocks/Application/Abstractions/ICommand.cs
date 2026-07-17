using MediatR;

namespace DayFlow.BuildingBlocks.Application.Messaging
{
    public interface ICommand<out TResponse>
      : IRequest<TResponse>
    {
    }
}
