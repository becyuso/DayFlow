using DayFlow.BuildingBlocks.Application.Behaviors;
using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.Modules.Notes.Infrastructure.Database;

namespace DayFlow.Modules.Notes.Infrastructure.Behaviors
{
    public sealed class NoteTransactionBehavior<TRequest, TResponse>
        : TransactionBehavior<TRequest, TResponse, NoteDbContext>
        where TRequest : ICommand<TResponse>
    {
        public NoteTransactionBehavior(
            NoteDbContext db)
            : base(db)
        {
        }
    }
}
