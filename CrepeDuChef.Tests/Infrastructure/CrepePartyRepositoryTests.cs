using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Infrastructure.Repositories;
using FluentAssertions;

namespace CrepeDuChef.Tests.Infrastructure
{
    public sealed class CrepePartyRepositoryTests : IDisposable
    {
        private readonly SqliteInMemoryDbContextFactory _factory = new();
        private readonly CrepePartyRepository _repo;

        private static readonly Guid DeviceId = Guid.Parse("00000000-0000-0000-9999-000000000001");
        private static readonly DateTime CreatedAt = new(2026, 05, 04, 12, 00, 00, DateTimeKind.Utc);

        public CrepePartyRepositoryTests()
        {
            _repo = new CrepePartyRepository(_factory);
        }

        public void Dispose() => _factory.Dispose();

        private static User NewChef(string firstName = "John", string lastName = "Doe") => new()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            UpdatedAt = CreatedAt,
            OriginDeviceId = DeviceId,
        };

        [Fact]
        public async Task UpdateChefAsync_Should_Persist_Names_And_UpdatedAt()
        {
            // Arrange
            User chef = NewChef();
            await _repo.AddChefAsync(chef);

            DateTime updatedAt = CreatedAt.AddDays(1);
            User update = NewChef("Jane", "Smith");
            update.Id = chef.Id;
            update.UpdatedAt = updatedAt;

            // Act
            await _repo.UpdateChefAsync(update);

            // Assert
            User? stored = await _repo.GetChefAsync(chef.Id);
            stored!.FirstName.Should().Be("Jane");
            stored.LastName.Should().Be("Smith");
            stored.UpdatedAt.Should().Be(updatedAt);
        }

        [Fact]
        public async Task GetLastSessionNumberAsync_Should_Return_Zero_When_No_Party()
        {
            // Act
            int last = await _repo.GetLastSessionNumberAsync();

            // Assert
            last.Should().Be(0);
        }

        [Fact]
        public async Task GetLastSessionNumberAsync_Should_Return_Highest_Session()
        {
            // Arrange
            User chef1 = NewChef("A", "A");
            User chef2 = NewChef("B", "B");
            await _repo.AddChefAsync(chef1);
            await _repo.AddChefAsync(chef2);

            await _repo.AddCrepePartyAsync(new CrepesParty
            {
                UserId = chef1.Id, SessionNumber = 3, Date = CreatedAt, UpdatedAt = CreatedAt, OriginDeviceId = DeviceId,
            });
            await _repo.AddCrepePartyAsync(new CrepesParty
            {
                UserId = chef2.Id, SessionNumber = 7, Date = CreatedAt, UpdatedAt = CreatedAt, OriginDeviceId = DeviceId,
            });

            // Act
            int last = await _repo.GetLastSessionNumberAsync();

            // Assert
            last.Should().Be(7);
        }
    }
}
