using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace DayFlow.Modules.Identity.Application;

/// <summary>
/// Application Layer DI 設定
/// 
/// 職責：
/// 註冊 MediatR
/// 掃描 Commands / Queries / Handlers
/// </summary>
public static class IdentityApplication
{
    public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(IdentityApplication).Assembly);
        });
        return services;
    }
}