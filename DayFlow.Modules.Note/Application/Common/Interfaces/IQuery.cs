using MediatR;

namespace DayFlow.Modules.Notes.Application.Common.Interfaces
{
    public interface IQuery<out TResponse>
        : IRequest<TResponse>
    {
    }
}
