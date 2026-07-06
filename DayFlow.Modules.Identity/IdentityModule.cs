using DayFlow.Modules.Identity.Application;
using DayFlow.Modules.Identity.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.Modules.Identity;

/// <summary>
/// Identity Module 對外唯一入口
/// 
/// 職責：
/// 1. 組合 Application + Infrastructure
/// 2. 提供單一 DI 入口（避免 API 了解內部結構）
/// </summary>
public static class IdentityModule
{
    /// <summary>
    /// 註冊 Identity Module 所有依賴
    /// </summary>
    public static IServiceCollection AddIdentityModule(
        this IServiceCollection services,
        IConfiguration config,
        string? connectionString = null)
    {
        // 註冊 Application 層（CQRS / MediatR）
        services.AddIdentityApplication();

        // 註冊 Infrastructure 層（EF Core / Repository ...）
        services.AddIdentityInfrastructure(config, connectionString);

        return services;
    }
}