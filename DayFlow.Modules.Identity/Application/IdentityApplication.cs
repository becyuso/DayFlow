using DayFlow.BuildingBlocks.Application.Behaviors;
using DayFlow.BuildingBlocks.Application.Messages;
using DayFlow.Modules.Identity.Application.Messages;
using DayFlow.Modules.Identity.Infrastructure.Behaviors;
using Microsoft.Extensions.DependencyInjection;

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

            cfg.AddOpenBehavior(
                typeof(MessageBehavior<,>));
            cfg.AddOpenBehavior(
                typeof(ExceptionBehavior<,>));            // 記錄任何未預期的例外
            cfg.AddOpenBehavior(
                typeof(ValidationBehavior<,>));           // 輸入驗證,失敗就不進資料庫
            cfg.AddOpenBehavior(
                typeof(DomainExceptionBehavior<,>));      // 攔截業務規則例外,轉成 Result.Failure
            cfg.AddOpenBehavior(
                typeof(IdentityTransactionBehavior<,>));  // 交易範圍,包住 Handler + SaveChanges
            //cfg.AddOpenBehavior(
            //    typeof(CachingBehavior<,>));
        });

        //掃描類型: 所有繼承
        //services.AddValidatorsFromAssembly(typeof(CreateValidator).Assembly);

        services
            .AddMessageRegistrar<CommonMessageRegistrar>();
        services
            .AddMessageRegistrar<CommonMessageRegistrar>();
        services
            .AddMessageRegistrar<IdentityMessageRegistrar>();

        return services;
    }
}