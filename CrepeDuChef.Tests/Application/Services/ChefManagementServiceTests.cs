using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Services;
using CrepeDuChef.Domain.Entities;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using NSubstitute;

namespace CrepeDuChef.Tests.Application.Services
{
    public class ChefManagementServiceTests
    {
        private readonly ICrepePartyRepository _repo;
        private readonly IValidator<UserDtoAdd> _addValidator;
        private readonly IValidator<UserDtoUpdate> _updateValidator;
        private readonly IDeviceIdProvider _deviceIdProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        Guid Id01Test;
        Guid Id02Test;
        Guid Id42Test;

        Guid DeviceIdTest;
        DateTime FixedNow;

        private ChefManagementService CreateService()
            => new(_repo, _addValidator, _updateValidator, _deviceIdProvider, _dateTimeProvider);


        public ChefManagementServiceTests()
        {
            _repo = Substitute.For<ICrepePartyRepository>();
            _addValidator = Substitute.For<IValidator<UserDtoAdd>>();
            _updateValidator = Substitute.For<IValidator<UserDtoUpdate>>();
            
            DeviceIdTest = Guid.Parse("00000000-0000-0000-9999-000000000001");
            FixedNow = new(2026, 05, 04, 12, 00, 00);

            Id01Test = Guid.Parse("00000000-0000-0000-0000-000000000001");
            Id02Test = Guid.Parse("00000000-0000-0000-0000-000000000002");
            Id42Test = Guid.Parse("00000000-0000-0000-0000-000000000042");
            
            _deviceIdProvider = Substitute.For<IDeviceIdProvider>();
            _deviceIdProvider.DeviceId.Returns(DeviceIdTest);

            _dateTimeProvider = Substitute.For<IDateTimeProvider>();
            _dateTimeProvider.UtcNow.Returns(FixedNow);
        }

        // -------------------------------------------------------
        // GET ALL
        // -------------------------------------------------------

        [Fact]
        public async Task GetAllUsersAsync_Should_Return_Empty_List_When_Repo_Returns_Empty()
        {
            // Arrange
            _repo.GetAllChefsAsync().Returns([]);

            var service = CreateService();

            // Act
            var result = await service.GetAllUsersAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllUsersAsync_Should_Map_Users_To_Dtos()
        {
            // Arrange
            _repo.GetAllChefsAsync().Returns(
            [
                new User { Id = Id01Test, FirstName = "John", LastName = "Doe" },
                new User { Id = Id02Test, FirstName = "Jane", LastName = "Smith" }
            ]);

            var service = CreateService();

            // Act
            var result = await service.GetAllUsersAsync();

            // Assert
            result.Should().HaveCount(2);
            result[0].FirstName.Should().Be("John");
            result[1].LastName.Should().Be("Smith");
        }

        // -------------------------------------------------------
        // ADD USER
        // -------------------------------------------------------

        [Fact]
        public async Task AddUserAsync_Should_Throw_When_Validation_Fails()
        {
            // Arrange
            UserDtoAdd dto = new() { FirstName = "", LastName = "" };

            _addValidator.Validate(dto).Returns(new ValidationResult([
                new ValidationFailure("FirstName", "Invalid")
            ]));

            var service = CreateService();

            // Act
            Func<Task> act = async () => await service.AddUserAsync(dto);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task AddUserAsync_Should_Add_User_And_Return_Dto()
        {
            // Arrange
            UserDtoAdd dto = new() { FirstName = "John", LastName = "Doe" };

            _addValidator.Validate(dto).Returns(new ValidationResult());

            var service = CreateService();

            // Act
            var result = await service.AddUserAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result!.FirstName.Should().Be("John");

            await _repo.Received(1).AddChefAsync(Arg.Is<User>(u =>
                u.FirstName == "John" &&
                u.LastName == "Doe"
            ));
        }

        // -------------------------------------------------------
        // UPDATE USER
        // -------------------------------------------------------

        [Fact]
        public async Task UpdateUserAsync_Should_Throw_When_Validation_Fails()
        {
            // Arrange
            UserDto existing = new() { Id = Id01Test, FirstName = "Old", LastName = "Name" };
            UserDtoUpdate update = new() { FirstName = "", LastName = "" };

            _updateValidator.Validate(update).Returns(new ValidationResult([
                new ValidationFailure("FirstName", "Invalid"),
                new ValidationFailure("LastName", "Invalid")
            ]));

            var service = CreateService();

            // Act
            Func<Task> act = async () => await service.UpdateUserAsync(existing, update);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task UpdateUserAsync_Should_Update_User_And_Return_Updated_Dto()
        {
            // Arrange
            UserDto existing = new() { Id = Id01Test, FirstName = "Old", LastName = "Name" };
            UserDtoUpdate update = new() { FirstName = "New", LastName = "Value" };

            _updateValidator.Validate(update).Returns(new ValidationResult());

            var service = CreateService();

            // Act
            var result = await service.UpdateUserAsync(existing, update);

            // Assert
            result.Should().NotBeNull();
            result!.FirstName.Should().Be("New");
            result.LastName.Should().Be("Value");

            await _repo.Received(1).UpdateChefAsync(Arg.Is<User>(u =>
                u.Id == Id01Test &&
                u.FirstName == "New" &&
                u.LastName == "Value"
            ));
        }


        [Fact]
        public async Task UpdateUserAsync_Should_Update_User_And_Return_Updated_Dto_With_Fixed_ID()
        {
            // Arrange
            UserDto existing = new() { Id = Id01Test, FirstName = "Old", LastName = "Name" };
            UserDtoUpdate update = new() { Id= Id42Test, FirstName = "New", LastName = "Value" };

            _updateValidator.Validate(update).Returns(new ValidationResult());

            var service = CreateService();

            // Act
            var result = await service.UpdateUserAsync(existing, update);

            // Assert
            result.Should().NotBeNull();
            result!.FirstName.Should().Be("New");
            result.LastName.Should().Be("Value");
            result.Id.Should().Be(Id01Test);

            await _repo.Received(1).UpdateChefAsync(Arg.Is<User>(u =>
                u.Id == Id01Test &&
                u.FirstName == "New" &&
                u.LastName == "Value"
            ));
        }
    }
}
