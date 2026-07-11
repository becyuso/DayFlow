using MediatR;

namespace DayFlow.Modules.Identity.Application.Features.Authentication.Login
{
    public record Command(string? Email, string? Password) : IRequest<Result>;
}
