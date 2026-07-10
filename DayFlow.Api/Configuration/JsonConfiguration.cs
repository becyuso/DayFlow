namespace DayFlow.Api.Configuration
{
    public static class JsonConfiguration
    {
        public static IServiceCollection AddJsonConfiguration(
            this IServiceCollection services)
        {
            services.AddJsonConfiguration();

            return services;
        }
    }
}
