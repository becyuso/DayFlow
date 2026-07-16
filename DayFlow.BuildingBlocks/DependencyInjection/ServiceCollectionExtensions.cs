using DayFlow.BuildingBlocks.Identity;
using DayFlow.BuildingBlocks.Time;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.BuildingBlocks.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBuildingBlocks(
            this IServiceCollection services,
            Action<BuildingBlockOptions>? configure = null)
        {
            var options = new BuildingBlockOptions();
            configure?.Invoke(options);

            if (options.UseIdentity)
                services.AddIdentityBuildingBlock();

            if (options.UseTime)
                services.AddTimeBuildingBlock();

            return services;
        }
    }
}