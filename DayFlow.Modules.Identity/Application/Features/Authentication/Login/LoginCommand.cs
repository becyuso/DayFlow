using MediatR;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace DayFlow.Modules.Identity.Application.Features.Authentication.Login
{
    public record LoginCommand(string? Email, string? Password) : IRequest<LoginResult>;

    public record LoginResult
    {
        public bool Success { get; init; }
        public string? ErrorMessage { get; init; }
        public long? UserId { get; init; }
        public Guid? PublicId { get; init; }
        public string? Email { get; init; }
        public string? DisplayName { get; init; }

        public static LoginResult Fail(string message) => new()
        {
            Success = false,
            ErrorMessage = message
        };

        public static LoginResult Ok(long userId, Guid publicId, string email, string displayName) => new()
        {
            Success = true,
            UserId = userId,
            PublicId = publicId,
            Email = email,
            DisplayName = displayName
            //result.AccessToken,
            //result.ExpireAt
        };
    }
}
