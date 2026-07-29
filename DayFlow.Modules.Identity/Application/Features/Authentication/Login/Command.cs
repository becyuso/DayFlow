using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.BuildingBlocks.Application.Results;

namespace DayFlow.Modules.Identity.Application.Features.Authentication.Login
{
    public sealed record LoginCommand(
        string? Email, 
        string? Password)
        : ICommand<Result<LoginResult>>;

    public sealed record LoginResult
    {
        public bool IsSuccess { get; init; }
        public string? ErrorMessage { get; init; }
        public long? UserId { get; init; }
        public Guid? PublicId { get; init; }
        public string? Email { get; init; }
        public string? DisplayName { get; init; }
    }
}
