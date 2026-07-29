using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DayFlow.Modules.Identity.Presentation.Api.Authentication.Login
{
    public static class Endpoint
    {
        public static IEndpointRouteBuilder MapAuthenticationLoginEndpoint(
            this IEndpointRouteBuilder app)
        {
            app.MapPost(
                    "/api/authentication/login",
                    HandleAsync)
                .AllowAnonymous()
                .WithName("Login")
                .WithTags("Authentication")
                .Produces<LoginResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status401Unauthorized);
            return app;
        }

        private static async Task<IResult> HandleAsync(
            [FromBody] LoginRequest request,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await sender.Send(
                command,
                cancellationToken);

            if (result.IsSuccess == false)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(result.Data);
        }
    }
}
