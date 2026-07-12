using DayFlow.Modules.Identity.Presentation.Api.Notebook.Create;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DayFlow.Modules.Note.Presentation.Api.Notebook.Create
{
    public static class Endpoint
    {
        public static IEndpointRouteBuilder MapNotebookCreateEndpoint(
            this IEndpointRouteBuilder app)
        {
            app.MapPost(
                    "/api/notebook/create",
                    HandleAsync)
                .AllowAnonymous()
                .WithName("Create")
                .WithTags("Notebook")
                .Produces<CreateResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status401Unauthorized);
            return app;
        }

        private static async Task<IResult> HandleAsync(
            [FromBody] CreateRequest request,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await sender.Send(
                command,
                cancellationToken);

            if (result.Success == false)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(result.ToResponse());
        }
    }
}
