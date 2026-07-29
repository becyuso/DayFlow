using DayFlow.BuildingBlocks.Application.Behaviors;
using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.Modules.Identity.Infrastructure.Database;

namespace DayFlow.Modules.Identity.Infrastructure.Behaviors
{
    public sealed class IdentityTransactionBehavior<TRequest, TResponse>
      : TransactionBehavior<TRequest, TResponse, IdentityDbContext>
      where TRequest : ICommand<TResponse>
    {
        public IdentityTransactionBehavior(
            IdentityDbContext db)
            : base(db)
        {
        }
    }
}
