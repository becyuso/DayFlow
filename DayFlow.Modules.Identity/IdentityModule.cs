using DayFlow.Modules.Identity.Application;
using DayFlow.Modules.Identity.Infrastructure;
using DayFlow.Web.Common.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.Modules.Identity;

public interface IModule
{
    void Register(IServiceCollection services, IConfiguration config, string? connectionString = null);
    void RegisterMvc(IMvcBuilder mvc);
}

/// <summary>
/// Identity Module 對外唯一入口
/// 
/// 職責：
/// 1. 組合 Application + Infrastructure
/// 2. 提供單一 DI 入口（避免 API 了解內部結構）
/// </summary>
public class IdentityModule : IModule
{
    /// <summary>
    /// 註冊 Identity Module 所有依賴
    /// </summary>
    public void Register(
        IServiceCollection services,
        IConfiguration config,
        string? connectionString = null)
    {
        // 註冊 Application 層（CQRS / MediatR）
        services.AddIdentityApplication();

        // 註冊 Infrastructure 層（EF Core / Repository ...）
        services.AddIdentityInfrastructure(config, connectionString);
    }

    /// <summary>
    /// Program.cs需要加上 app.MapRazorPages(); 
    /// </summary>
    /// <param name="mvc"></param>
    public void RegisterMvc(IMvcBuilder mvc)
    {
        mvc.ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(
                new NamespaceControllerFeatureProvider(
                    "DayFlow.Modules.Identity.Presentation.Web"));
        }).AddApplicationPart(typeof(IdentityModule).Assembly);

        mvc.AddRazorOptions(options =>
        {
            options.ViewLocationExpanders
            .Add(
              new IdentityViewLocationExpander()
            );
        });
    }

    public class IdentityViewLocationExpander
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

            var locations = new[]
            {
               "/Presentation/Web/Views/{1}/{0}.cshtml",
               "/Presentation/Web/Views/Shared/{0}.cshtml"
            };

            return locations.Concat(viewLocations);
        }
    }
}