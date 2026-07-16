using DayFlow.Modules.Notes.Application;
using DayFlow.Modules.Notes.Infrastructure;
using DayFlow.Web.Common.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.Modules.Notes;

/// <summary>
/// Note Module 對外唯一入口
/// 
/// 職責：
/// 1. 組合 Application + Infrastructure
/// 2. 提供單一 DI 入口（避免 API 了解內部結構）
/// </summary>
public static class NoteModule
{
    /// <summary>
    /// 註冊 Note Module 所有依賴
    /// </summary>
    public static IServiceCollection AddNoteModule(
        this IServiceCollection services,
        IConfiguration config,
        string? connectionString = null)
    {
        // 註冊 Application 層（CQRS / MediatR）
        services.AddNoteApplication();

        // 註冊 Infrastructure 層（EF Core / Repository ...）
        services.AddNoteInfrastructure(config, connectionString);

        return services;
    }

    /// <summary>
    /// Program.cs需要加上 app.MapRazorPages(); 
    /// </summary>
    /// <param name="mvc"></param>
    public static IMvcBuilder AddNotePresentation(
        this IMvcBuilder mvc)
    {
        mvc.ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(
                new NamespaceControllerFeatureProvider(
                    $"{typeof(NoteModule).Assembly.GetName().Name}.Presentation.Web"));
        }).AddApplicationPart(typeof(NoteModule).Assembly);

        mvc.AddRazorOptions(options =>
        {
            options.ViewLocationExpanders
            .Add(
                new NoteViewLocationExpander()
            );
        });

        return mvc;
    }

    public class NoteViewLocationExpander
    : IViewLocationExpander
    {
        public void PopulateValues(
            ViewLocationExpanderContext context)
        {
        }

        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            var features = new[]
            {
                "Notebook"
            };

            IEnumerable<string> locations = features.SelectMany(m => new[]
            {
                $"/Presentation/Web/{m}/Views/{{1}}/{{0}}.cshtml",
            });

            return locations.Concat(viewLocations);
        }
    }
}