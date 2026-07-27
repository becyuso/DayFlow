using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DayFlow.BuildingBlocks.Application.Messages
{
    public static class MessageRegistrationExtensions
    {
        public static IServiceCollection AddMessageRegistrar<T>(
            this IServiceCollection services)
            where T : class, IMessageRegistrar
        {
            services.TryAddEnumerable(
                ServiceDescriptor.Singleton<
                IMessageRegistrar,
                T>());
            //services.AddSingleton<
            //    IMessageRegistrar,
            //    T>();

            services.TryAddSingleton<
                IMessageCatalog,
                MessageCatalog>();

            return services;
        }
    }
}
