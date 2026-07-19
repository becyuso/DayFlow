using DayFlow.Modules.Identity.Infrastructure.Database;
using DayFlow.Modules.Notes.Domain.Entities;
using DayFlow.Modules.Notes.Infrastructure.Database;
using DayFlow.Test.DayFlow.Modules.Identity.Test.TestData.Builders;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DayFlow.Test.DayFlow.Modules.Note.Test.TestData.Seeds;

public static class NotebookSeed
{
    public static async Task SeedAsync(NoteDbContext db)
    {
        if (db.Notebooks.Any())
            return;

        //var users = Enumerable.Range(1, 30)
        //    .Select(i => new UserBuilder()
        //        .WithEmail($"user{i:D2}@company.com")
        //        .WithDisplayName($"User {i:D2}")
        //        .WithStatus(i % 3 == 0 ? (short)1 : (short)0) // 模擬啟用/停用
        //        .WithLastLogin(DateTime.UtcNow.AddDays(-i))
        //        .Build()
        //    )
        //    .ToList();
        var notebook = Enumerable.Range(1, 30)
            .Select(i =>
            Notebook.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            $"工作筆記{i:D2}",
            "#FF0000",
            1,
            DateTime.UtcNow,
            Guid.NewGuid(),
            DateTime.UtcNow,
            Guid.NewGuid())
        ).ToList();

        //NotebookId = Guid.NewGuid(),
        //UserId = Guid.NewGuid(),
        //Name = $"工作筆記{i:D2}",
        //Color = "#FF0000",
        //SortOrder = 1,
        //CreatedAt = DateTime.UtcNow,
        //CreatedBy = Guid.NewGuid(),
        //UpdatedAt = DateTime.UtcNow,
        //UpdatedBy = Guid.NewGuid()

        await db.Notebooks.AddRangeAsync(notebook);
        await db.SaveChangesAsync();
    }
}