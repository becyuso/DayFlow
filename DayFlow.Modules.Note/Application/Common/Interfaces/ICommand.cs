using MediatR;

namespace DayFlow.Modules.Notes.Application.Common.Interfaces
{
    public interface ICommand<out TResponse>
      : IRequest<TResponse>
    {
    }
}
