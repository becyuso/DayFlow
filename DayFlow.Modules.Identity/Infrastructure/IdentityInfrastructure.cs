using DayFlow.BuildingBlocks.Application.Abstractions;
using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.Modules.Identity.Domain.Users;
using DayFlow.Modules.Identity.Infrastructure.Database;
using DayFlow.Modules.Identity.Infrastructure.Repositories;
using DayFlow.Modules.Identity.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.Modules.Identity.Infrastructure;

/// <summary>
/// Infrastructure Layer DI 設定
/// 
/// 職責：
/// 1. EF Core DbContext
/// 2. Repository 實作
/// 3. 外部資源（DB / Redis / File 等）
/// </summary>
public static class IdentityInfrastructure
{
    public static IServiceCollection AddIdentityInfrastructure(
        this IServiceCollection services,
        IConfiguration config,
        string? connectionString = null)
    {
        // --------------------------------------------------
        // DbContext 註冊
        // --------------------------------------------------
        services.AddDbContext<IdentityDbContext>(options =>
        {
            if (!string.IsNullOrWhiteSpace(connectionString)) // Production：使用 SQL Server
            {
                options.UseSqlServer(connectionString); 
            }
            else // Development / Test：使用 InMemory DB
            {
                options.UseInMemoryDatabase("DayFlow.Identity.InMemory");
            }
        });

        // --------------------------------------------------
        // Repository 註冊
        // --------------------------------------------------
        // Scoped = 每個 request 一個 DbContext lifecycle
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddSecurity(config);

        return services;
    }
}