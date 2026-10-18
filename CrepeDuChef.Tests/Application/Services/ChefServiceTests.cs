using CrepeDuChef.Application.Common;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Services;
using CrepeDuChef.Application.Validation;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Localization.Resources;
using CrepeDuChef.Tests.Helpers;
using FluentAssertions;
using NSubstitute;

namespace CrepeDuChef.Tests.Application.Services
{
    public class ChefServiceTests
    {
        private static readonly Guid Id01 = Guid.Parse("00000000-0000-0000-0000-000000000001");
        private static readonly Guid Id02 = Guid.Parse("00000000-0000-0000-0000-000000000002");
        private static readonly Guid DeviceId = Guid.Parse("00000000-0000-0000-9999-000000000001");
        private static readonly DateTime FixedNow = new(2026, 05, 04, 12, 00, 00, DateTimeKind.Utc);

        private readonly IChefRepository _repo = Substitute.For<IChefRepository>();
        private readonly ChefService _service;

        public ChefServiceTests()
        {
            IDeviceIdProvider deviceId = Substitute.For<IDeviceIdProvider>();
            deviceId.DeviceId.Returns(DeviceId);

            IDateTimeProvider dateTime = Substitute.For<IDateTimeProvider>();
            dateTime.UtcNow.Returns(FixedNow);

            FakeLocalizer<ValidationResources> localizer = new();

            _service = new ChefService(
                _repo,
                new AddChefDtoValidator(localizer),
                new UpdateChefDtoValidator(localizer),
                deviceId,
                dateTime);
        }

        // -------------------------------------------------------
        // GET ALL
        // -------------------------------------------------------

        [Fact]
        public async Task GetAllAsync_Should_Return_Empty_List_When_Repo_Returns_Empty()
        {
            // Arrange
            _repo.GetAllAsync().Returns([]);

            // Act
            IReadOnlyList<ChefDto> result = await _service.GetAllAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_Should_Map_Chefs_To_Dtos()
        {
            // Arrange
            _repo.GetAllAsync().Returns(
            [
                new Chef { Id = Id01, FirstName = "John", LastName = "Doe" },
                new Chef { Id = Id02, FirstName = "Jane", LastName = "Smith" }
            ]);

            // Act
            IReadOnlyList<ChefDto> result = await _service.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(
            [
                new ChefDto { Id = Id01, FirstName = "John", LastName = "Doe" },
                new ChefDto { Id = Id02, FirstName = "Jane", LastName = "Smith" }
            ]);
        }

        // -------------------------------------------------------
        // ADD
        // -------------------------------------------------------

        [Fact]
        public async Task AddAsync_Should_Fail_And_Not_Persist_When_Validation_Fails()
        {
            // Act
            Result<ChefDto> result = await _service.AddAsync(new AddChefDto { FirstName = "", LastName = "" });

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().BeEquivalentTo(["FirstNameRequired", "LastNameRequired"]);
            await _repo.DidNotReceiveWithAnyArgs().AddAsync(default!);
        }

        [Fact]
        public async Task AddAsync_Should_Persist_Chef_With_Sync_Fields_And_Return_Its_Dto()
        {
            // Arrange
            Chef? persisted = null;
            await _repo.AddAsync(Arg.Do<Chef>(c => persisted = c));

            // Act
            Result<ChefDto> result = await _service.AddAsync(new AddChefDto { FirstName = "John", LastName = "Doe" });

            // Assert
            result.IsSuccess.Should().BeTrue();

            persisted.Should().NotBeNull();
            persisted!.Id.Should().NotBeEmpty();
            persisted.FullName.Should().Be("John Doe");
            persisted.UpdatedAt.Should().Be(FixedNow);
            persisted.OriginDeviceId.Should().Be(DeviceId);

            result.Value.Should().Be(new ChefDto { Id = persisted.Id, FirstName = "John", LastName = "Doe" });
        }

        // -------------------------------------------------------
        // UPDATE
        // -------------------------------------------------------

        [Fact]
        public async Task UpdateAsync_Should_Fail_And_Not_Persist_When_Validation_Fails()
        {
            // Act
            Result<ChefDto> result = await _service.UpdateAsync(new UpdateChefDto { Id = Id01, FirstName = "", LastName = "Doe" });

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().BeEquivalentTo(["FirstNameRequired"]);
            await _repo.DidNotReceiveWithAnyArgs().UpdateAsync(default!);
        }

        [Fact]
        public async Task UpdateAsync_Should_Fail_When_Id_Is_Empty()
        {
            // Act
            Result<ChefDto> result = await _service.UpdateAsync(new UpdateChefDto { Id = Guid.Empty, FirstName = "John", LastName = "Doe" });

            // Assert
            result.IsSuccess.Should().BeFalse();
            await _repo.DidNotReceiveWithAnyArgs().UpdateAsync(default!);
        }

        [Fact]
        public async Task UpdateAsync_Should_Persist_Changes_And_Return_Updated_Dto()
        {
            // Act
            Result<ChefDto> result = await _service.UpdateAsync(new UpdateChefDto { Id = Id01, FirstName = "New", LastName = "Value" });

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(new ChefDto { Id = Id01, FirstName = "New", LastName = "Value" });

            await _repo.Received(1).UpdateAsync(Arg.Is<Chef>(c =>
                c.Id == Id01 &&
                c.FirstName == "New" &&
                c.LastName == "Value" &&
                c.UpdatedAt == FixedNow));
        }
    }
}
