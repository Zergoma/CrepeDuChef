using CrepeDuChef.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CrepeDuChef.Tests.Infrastructure
{
    /// <summary>
    /// Creates contexts sharing one open in-memory SQLite connection, so data survives
    /// across the short-lived contexts the repositories create.
    /// </summary>
    public sealed class SqliteInMemoryDbContextFactory : IDbContextFactory<CrepeDbContext>, IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<CrepeDbContext> _options;

        public SqliteInMemoryDbContextFactory()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<CrepeDbContext>()
                .UseSqlite(_connection)
                .Options;

            using var ctx = CreateDbContext();
            ctx.Database.EnsureCreated();
        }

        public CrepeDbContext CreateDbContext() => new(_options);

        public void Dispose() => _connection.Dispose();
    }
}
