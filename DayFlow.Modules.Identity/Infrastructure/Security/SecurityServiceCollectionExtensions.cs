using DayFlow.Modules.Identity.Application.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.Modules.Identity.Infrastructure.Security
{
    public static class SecurityServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurity(
            this IServiceCollection services,
            IConfiguration config)
        {
            // bind appsettings.json
            // error cs1503 =>　dotnet add package Microsoft.Extensions.Options.ConfigurationExtensions 2026/07/06
            services.Configure<Argon2PasswordHasherOptions>(
                config.GetSection("Security:Argon2PasswordHasher"));

            // Password hasher
            services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
            return services;
        }
    }
}
