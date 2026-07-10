namespace DayFlow.Api.Configuration
{
    public static class ProblemDetailsConfiguration
    {
        public static IServiceCollection AddProblemDetailsConfiguration(
            this IServiceCollection services)
        {
            services.AddProblemDetails();

            return services;
        }
    }
}
