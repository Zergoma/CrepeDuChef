using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrepeDuChef.Tests.Infrastructure
{
    public sealed class MigrationTests : IDisposable
    {
        private const string BeforeChefRename = "20261018032413_AlignUserNameMaxLength";

        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<CrepeDbContext> _options;

        public MigrationTests()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<CrepeDbContext>()
                .UseSqlite(_connection)
                .Options;
        }

        public void Dispose() => _connection.Dispose();

        [Fact]
        public void RenameUserToChef_Should_Keep_Existing_Data()
        {
            // Arrange: a database at the schema prior to the rename, with data
            Guid chefId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            Guid partyId = Guid.Parse("00000000-0000-0000-0000-000000000101");
            Guid deviceId = Guid.Parse("00000000-0000-0000-9999-000000000001");

            // EF stores Guids as upper-case TEXT; raw Guid parameters would be bound as BLOB
            string chef = chefId.ToString().ToUpperInvariant();
            string party = partyId.ToString().ToUpperInvariant();
            string device = deviceId.ToString().ToUpperInvariant();

            using (CrepeDbContext ctx = new(_options))
            {
                ctx.GetService<IMigrator>().Migrate(BeforeChefRename);

                ctx.Database.ExecuteSql($"""
                    INSERT INTO Users (Id, FirstName, LastName, UpdatedAt, OriginDeviceId)
                    VALUES ({chef}, 'John', 'Doe', '2026-05-04 12:00:00', {device});
                    """);
                ctx.Database.ExecuteSql($"""
                    INSERT INTO CrepesParty (Id, Date, SessionNumber, UserId, UpdatedAt, OriginDeviceId)
                    VALUES ({party}, '2026-05-04 12:00:00', 3, {chef}, '2026-05-04 12:00:00', {device});
                    """);
            }

            // Act
            using (CrepeDbContext ctx = new(_options))
            {
                ctx.Database.Migrate();
            }

            // Assert
            using (CrepeDbContext ctx = new(_options))
            {
                Chef storedChef = ctx.Chefs.Single();
                storedChef.Id.Should().Be(chefId);
                storedChef.FullName.Should().Be("John Doe");

                CrepeParty storedParty = ctx.CrepeParties.Include(cp => cp.Chef).Single();
                storedParty.Id.Should().Be(partyId);
                storedParty.SessionNumber.Should().Be(3);
                storedParty.Chef!.Id.Should().Be(chefId);

                ctx.Database.GetPendingMigrations().Should().BeEmpty();
            }
        }
    }
}
