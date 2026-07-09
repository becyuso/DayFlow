namespace DayFlow.Modules.Identity.Presentation.Api.Authentication.Login;

public sealed record LoginRequest
(
    string? Account,
    string? Password
);
