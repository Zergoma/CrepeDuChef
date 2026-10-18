using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Services;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Tests.Helpers;
using FluentAssertions;
using NSubstitute;

namespace CrepeDuChef.Tests.Application.Services
{
    public class CrepePartyServiceTests
    {
        private static readonly Guid DeviceId = Guid.Parse("00000000-0000-0000-9999-000000000001");
        private static readonly DateTime FixedNow = new(2026, 05, 04, 12, 00, 00, DateTimeKind.Utc);

        private readonly ICrepePartyRepository _crepeParties = Substitute.For<ICrepePartyRepository>();
        private readonly IChefRepository _chefs = Substitute.For<IChefRepository>();
        private readonly CrepePartyService _service;

        public CrepePartyServiceTests()
        {
            IDeviceIdProvider deviceId = Substitute.For<IDeviceIdProvider>();
            deviceId.DeviceId.Returns(DeviceId);

            IDateTimeProvider dateTime = Substitute.For<IDateTimeProvider>();
            dateTime.UtcNow.Returns(FixedNow);

            _service = new CrepePartyService(_crepeParties, _chefs, deviceId, dateTime);
        }

        private static CrepeParty Party(int chef, int session, int day) => new()
        {
            ChefId = TestData.IntToGuid(chef),
            SessionNumber = session,
            Date = new DateTime(2026, 01, day, 0, 0, 0, DateTimeKind.Utc),
        };

        [Fact]
        public async Task GetSessionsAsync_Should_Group_By_Session_Most_Recent_First()
        {
            // Arrange
            _chefs.GetAllAsync().Returns(
            [
                new Chef { Id = TestData.IntToGuid(1), FirstName = "John", LastName = "Doe" },
                new Chef { Id = TestData.IntToGuid(2), FirstName = "Jane", LastName = "Smith" },
            ]);
            _crepeParties.GetAllAsync().Returns([Party(1, 1, 1), Party(2, 2, 3), Party(1, 2, 4), Party(2, 1, 2)]);

            // Act
            IReadOnlyList<CrepePartySessionDto> sessions = await _service.GetSessionsAsync();

            // Assert
            sessions.Select(s => s.SessionNumber).Should().Equal(2, 1);
            sessions[0].Entries.Select(e => e.ChefName).Should().Equal("John Doe", "Jane Smith");
            sessions[0].Entries.Select(e => e.DateUtc.Day).Should().Equal(4, 3);
        }

        [Fact]
        public async Task GetSessionsAsync_Should_Return_Null_Name_For_Unknown_Chef()
        {
            // Arrange
            _chefs.GetAllAsync().Returns([]);
            _crepeParties.GetAllAsync().Returns([Party(9, 1, 1)]);

            // Act
            IReadOnlyList<CrepePartySessionDto> sessions = await _service.GetSessionsAsync();

            // Assert
            CrepePartyEntryDto entry = sessions.Single().Entries.Single();
            entry.ChefId.Should().Be(TestData.IntToGuid(9));
            entry.ChefName.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_Should_Persist_With_Application_Date_And_Sync_Fields()
        {
            // Act
            await _service.AddAsync(new AddCrepePartyDto { ChefId = TestData.IntToGuid(1), SessionNumber = 4 });

            // Assert
            await _crepeParties.Received(1).AddAsync(Arg.Is<CrepeParty>(cp =>
                cp.Id != Guid.Empty &&
                cp.ChefId == TestData.IntToGuid(1) &&
                cp.SessionNumber == 4 &&
                cp.Date == FixedNow &&
                cp.UpdatedAt == FixedNow &&
                cp.OriginDeviceId == DeviceId));
        }
    }
}
