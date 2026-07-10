using DayFlow.Modules.Identity.Application;
using DayFlow.Modules.Identity.Infrastructure;
using DayFlow.Modules.Identity.Presentation.Api.Authentication.Login;
using DayFlow.Web.Common.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.Modules.Identity
{
    public static class IdentityEndpoint
    {
        public static IEndpointRouteBuilder MapIdentityEndpoints(
            this IEndpointRouteBuilder app)
        {
            app.MapLoginEndpoint();

            return app;
        }

        public static IServiceCollection AddIdentityEndpoint(
            this IServiceCollection services,
            IConfiguration config,
            string? connectionString = null)
        {

            // 註冊 Application 層（CQRS / MediatR）
            services.AddIdentityApplication();

            // 註冊 Infrastructure 層（EF Core / Repository ...）
            services.AddIdentityInfrastructure(config, connectionString);

            return services;
        }
    }
}
