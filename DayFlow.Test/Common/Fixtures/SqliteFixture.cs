using DayFlow.Modules.Identity.Infrastructure.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DayFlow.Test.Common.Fixtures
{
    public sealed class SqliteFixture<TContext>
        : IDisposable
        where TContext : DbContext
    {
        public TContext Context { get; }

        private readonly SqliteConnection _connection;

        public SqliteFixture(
        Func<DbContextOptions<TContext>, TContext> factory)
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options =
                new DbContextOptionsBuilder<TContext>()
                .UseSqlite(_connection)
                .EnableSensitiveDataLogging()
                .Options;

            Context = factory(options);

            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Dispose();
            _connection.Close();
        }
    }
}
