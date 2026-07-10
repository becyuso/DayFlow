using DayFlow.Modules.Identity;

namespace DayFlow.Api.Configuration
{
    public static class ModuleConfiguration
    {
        public static IServiceCollection AddModuleConfiguration(
            this IServiceCollection services,
            IConfiguration config,
            string? connectionString = null)
        {
            services.AddIdentityEndpoint(config, connectionString);

            return services;
        }
    }
}
