namespace DayFlow.Modules.Identity.Presentation.Api.Authentication.Login;

public sealed record LoginResponse
(
   long? UserId,
   Guid? PublicId,
   string? Email,
   string? DisplayName
);
