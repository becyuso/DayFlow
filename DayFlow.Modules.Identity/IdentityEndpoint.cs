using DayFlow.Web.Common.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.Modules.Identity
{
    public static class IdentityEndpoint
    {
        public static IServiceCollection AddIdentityPresentation(
            this IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(
                    new NamespaceControllerFeatureProvider(
                        "DayFlow.Modules.Identity.Presentation.Web"));
            })
                .AddApplicationPart(
                    typeof(IdentityModule).Assembly);

            return services;
        }
    }
}
