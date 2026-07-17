using DayFlow.BuildingBlocks.Application.Abstractions;
using DayFlow.BuildingBlocks.Application.Messaging;
using DayFlow.Modules.Notes.Infrastructure.Database;
using DayFlow.Modules.Notes.Infrastructure.Persistence;
using DayFlow.Modules.Notes.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayFlow.Modules.Notes.Infrastructure;

/// <summary>
/// Infrastructure Layer DI 設定
/// 
/// 職責：
/// 1. EF Core DbContext
/// 2. Repository 實作
/// 3. 外部資源（DB / Redis / File 等）
/// </summary>
public static class NoteInfrastructure
{
    public static IServiceCollection AddNoteInfrastructure(
        this IServiceCollection services,
        IConfiguration config,
        string? connectionString = null)
    {
        // --------------------------------------------------
        // DbContext 註冊
        // --------------------------------------------------
        services.AddDbContext<NoteDbContext>(options =>
        {
            if (!string.IsNullOrWhiteSpace(connectionString)) // Production：使用 SQL Server
            {
                options.UseSqlServer(connectionString);
            }
            else // Development / Test：使用 InMemory DB
            {
                options.UseInMemoryDatabase("DayFlow.Note.InMemory");
            }
        });

        // --------------------------------------------------
        // Repository 註冊
        // --------------------------------------------------
        // Scoped = 每個 request 一個 DbContext lifecycle
        services.AddScoped<INotebookRepository, NotebookRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<INoteTagRepository, NoteTagRepository>();
        services.AddScoped<ITagRepository, TagRepository>();


        services.AddScoped<ITransactionManager, EfTransactionManager>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}