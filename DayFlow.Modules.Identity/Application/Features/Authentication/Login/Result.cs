namespace DayFlow.Modules.Identity.Application.Features.Authentication.Login
{
    public record Result
    {
        public bool Success { get; init; }
        public string? ErrorMessage { get; init; }
        public long? UserId { get; init; }
        public Guid? PublicId { get; init; }
        public string? Email { get; init; }
        public string? DisplayName { get; init; }

        public static Result Fail(string message) => new()
        {
            Success = false,
            ErrorMessage = message
        };

        public static Result Ok(long userId, Guid publicId, string email, string displayName) => new()
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
