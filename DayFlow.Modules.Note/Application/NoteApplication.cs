using DayFlow.BuildingBlocks.Application.Behaviors;
using DayFlow.Modules.Notes.Application.Features.Notebook.Create;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.Modules.Notes.Application;

/// <summary>
/// Application Layer DI 設定
/// 
/// 職責：
/// 註冊 MediatR
/// 掃描 Commands / Queries / Handlers
/// </summary>
public static class NoteApplication
{
    public static IServiceCollection AddNoteApplication(this IServiceCollection services)
    {
        //掃描類型: IRequestHandler<> 等...
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(NoteApplication).Assembly);

            cfg.AddOpenBehavior(
                typeof(ExceptionBehavior<,>));         // 最外層:記錄任何未預期的例外
            cfg.AddOpenBehavior(
                typeof(ValidationBehavior<,>));         // 第二層:輸入驗證,失敗就不進資料庫
            cfg.AddOpenBehavior(
                typeof(DomainExceptionBehavior<,>));    // 第三層:攔截業務規則例外,轉成 Result.Failure
            cfg.AddOpenBehavior(
                typeof(TransactionBehavior<,>));        // 最內層:交易範圍,包住 Handler + SaveChanges
            //cfg.AddOpenBehavior(
            //    typeof(CachingBehavior<,>));

        });

        //掃描類型: 所有繼承
        services.AddValidatorsFromAssembly(typeof(CreateValidator).Assembly);

        return services;
    }
}