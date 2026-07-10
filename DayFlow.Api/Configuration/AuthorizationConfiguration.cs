namespace DayFlow.Api.Configuration
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddAuthorizationConfiguration(
            this IServiceCollection services)
        {
            services.AddAuthorization();

            return services;
        }
    }
}
