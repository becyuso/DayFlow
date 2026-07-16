using DayFlow.Modules.Notes.Application.Behaviors;
using DayFlow.Modules.Notes.Application.Features.Noteboke.Create;
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

            //cfg.AddOpenBehavior(
            //    typeof(LoggingBehavior<,>));

            cfg.AddOpenBehavior(
                typeof(ValidationBehavior<,>));

            cfg.AddOpenBehavior(
                typeof(TransactionBehavior<,>));

            //cfg.AddOpenBehavior(
            //    typeof(CachingBehavior<,>));
        });

        //掃描類型: 所有繼承
        services.AddValidatorsFromAssembly(typeof(CreateValidator).Assembly);

        return services;
    }
}