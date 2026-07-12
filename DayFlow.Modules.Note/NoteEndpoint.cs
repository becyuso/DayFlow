using DayFlow.Modules.Note.Application;
using DayFlow.Modules.Note.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.Modules.Note
{
    public static class NoteEndpoint
    {
        public static IEndpointRouteBuilder MapNoteEndpoints(
            this IEndpointRouteBuilder app)
        {
            //app.MapNotebookCreateEndpoint();

            return app;
        }

        public static IServiceCollection AddNoteEndpoint(
            this IServiceCollection services,
            IConfiguration config,
            string? connectionString = null)
        {

            // 註冊 Application 層（CQRS / MediatR）
            services.AddNoteApplication();

            // 註冊 Infrastructure 層（EF Core / Repository ...）
            services.AddNoteInfrastructure(config, connectionString);

            return services;
        }
    }
}

