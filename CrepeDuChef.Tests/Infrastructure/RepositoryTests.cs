using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Infrastructure.Repositories;
using FluentAssertions;

namespace CrepeDuChef.Tests.Infrastructure
{
    public sealed class RepositoryTests : IDisposable
    {
        private readonly SqliteInMemoryDbContextFactory _factory = new();
        private readonly ChefRepository _chefs;
        private readonly CrepePartyRepository _crepeParties;

        private static readonly Guid DeviceId = Guid.Parse("00000000-0000-0000-9999-000000000001");
        private static readonly DateTime CreatedAt = new(2026, 05, 04, 12, 00, 00, DateTimeKind.Utc);

        public RepositoryTests()
        {
            _chefs = new ChefRepository(_factory);
            _crepeParties = new CrepePartyRepository(_factory);
        }

        public void Dispose() => _factory.Dispose();

        private static Chef NewChef(string firstName = "John", string lastName = "Doe") => new()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            UpdatedAt = CreatedAt,
            OriginDeviceId = DeviceId,
        };

        private static CrepeParty NewParty(Chef chef, int session, DateTime date) => new()
        {
            Id = Guid.NewGuid(),
            ChefId = chef.Id,
            SessionNumber = session,
            Date = date,
            UpdatedAt = date,
            OriginDeviceId = DeviceId,
        };

        [Fact]
        public async Task ChefRepository_UpdateAsync_Should_Persist_Names_And_UpdatedAt()
        {
            // Arrange
            Chef chef = NewChef();
            await _chefs.AddAsync(chef);

            DateTime updatedAt = CreatedAt.AddDays(1);
            Chef update = NewChef("Jane", "Smith");
            update.Id = chef.Id;
            update.UpdatedAt = updatedAt;

            // Act
            await _chefs.UpdateAsync(update);

            // Assert
            Chef? stored = await _chefs.GetByIdAsync(chef.Id);
            stored!.FirstName.Should().Be("Jane");
            stored.LastName.Should().Be("Smith");
            stored.UpdatedAt.Should().Be(updatedAt);
        }

        [Fact]
        public async Task CrepePartyRepository_GetLastSessionNumberAsync_Should_Return_Zero_When_No_Party()
        {
            // Act
            int last = await _crepeParties.GetLastSessionNumberAsync();

            // Assert
            last.Should().Be(0);
        }

        [Fact]
        public async Task CrepePartyRepository_GetLastSessionNumberAsync_Should_Return_Highest_Session()
        {
            // Arrange
            Chef chef1 = NewChef("A", "A");
            Chef chef2 = NewChef("B", "B");
            await _chefs.AddAsync(chef1);
            await _chefs.AddAsync(chef2);

            await _crepeParties.AddAsync(NewParty(chef1, 3, CreatedAt));
            await _crepeParties.AddAsync(NewParty(chef2, 7, CreatedAt));

            // Act
            int last = await _crepeParties.GetLastSessionNumberAsync();

            // Assert
            last.Should().Be(7);
        }

        [Fact]
        public async Task CrepePartyRepository_GetBySessionAsync_Should_Return_Session_Parties_Oldest_First()
        {
            // Arrange
            Chef chef1 = NewChef("A", "A");
            Chef chef2 = NewChef("B", "B");
            await _chefs.AddAsync(chef1);
            await _chefs.AddAsync(chef2);

            await _crepeParties.AddAsync(NewParty(chef2, 1, CreatedAt.AddDays(1)));
            await _crepeParties.AddAsync(NewParty(chef1, 1, CreatedAt));
            await _crepeParties.AddAsync(NewParty(chef1, 2, CreatedAt.AddDays(2)));

            // Act
            List<CrepeParty> session = await _crepeParties.GetBySessionAsync(1);

            // Assert
            session.Select(cp => cp.ChefId).Should().Equal(chef1.Id, chef2.Id);
        }
    }
}
