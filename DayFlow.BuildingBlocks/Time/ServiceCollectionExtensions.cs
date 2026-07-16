using global::DayFlow.BuildingBlocks.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.BuildingBlocks.Time
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddTimeBuildingBlock(
            this IServiceCollection services)
        {

            services.AddSingleton<IClock, SystemClock>();

            return services;
        }

    }
}
