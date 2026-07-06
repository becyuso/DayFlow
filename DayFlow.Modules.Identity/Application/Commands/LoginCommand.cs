using MediatR;

namespace DayFlow.Modules.Identity.Application.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<LoginResult>;

    public record LoginResult
    {
        public bool Success { get; init; }
        public string? ErrorMessage { get; init; }
        public string? Email { get; init; }
        public string? DisplayName { get; init; }

        public static LoginResult Fail(string message) => new()
        {
            Success = false,
            ErrorMessage = message
        };

        public static LoginResult Ok(string email, string displayName) => new()
        {
            Success = true,
            Email = email,
            DisplayName = displayName
        };
    }
}
