using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.BuildingBlocks.Identity
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddIdentityBuildingBlock(
            this IServiceCollection services)
        {

            services.AddSingleton<IIdGenerator, UuidV7Generator>();

            return services;
        }

    }
}
