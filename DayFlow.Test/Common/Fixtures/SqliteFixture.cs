using DayFlow.Modules.Identity.Infrastructure.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Test.Common.Fixtures
{
    public class SqliteFixture : IDisposable
    {
        public IdentityDbContext Context { get; }

        private readonly SqliteConnection _connection;

        public SqliteFixture()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var option = new DbContextOptionsBuilder<IdentityDbContext>()
             .UseSqlite(_connection)
             .EnableSensitiveDataLogging()
             .Options;

            Context = new IdentityDbContext(option);

            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Dispose();
            _connection.Close();
        }
    }
}
