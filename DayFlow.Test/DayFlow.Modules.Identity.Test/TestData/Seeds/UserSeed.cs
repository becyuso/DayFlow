using DayFlow.Modules.Identity.Domain.Entities;
using DayFlow.Modules.Identity.Infrastructure.Database;
using DayFlow.Test.DayFlow.Modules.Identity.Test.TestData.Builders;

namespace DayFlow.Test.DayFlow.Modules.Identity.Test.TestData.Seeds
{
    public static class UserSeed
    {
        public static async Task SeedAsync(IdentityDbContext db)
        {
            if (db.Users.Any())
                return;

            var users = Enumerable.Range(1, 30)
                .Select(i => new UserBuilder()
                    .WithEmail($"user{i:D2}@company.com")
                    .WithDisplayName($"User {i:D2}")
                    .WithStatus(i % 3 == 0 ? (short)1 : (short)0) // 模擬啟用/停用
                    .WithLastLogin(DateTime.UtcNow.AddDays(-i))
                    .Build()
                )
                .ToList();

            await db.Users.AddRangeAsync(users);
            await db.SaveChangesAsync();
        }
    }
}
