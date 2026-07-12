using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.Modules.Note.Application;

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
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(NoteApplication).Assembly);
        });
        return services;
    }
}