using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Services;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Domain.Interfaces;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace CrepeDuChef.Tests.Application.Services
{
    public class ChefManagementServiceTests
    {
        private readonly ICrepePartyRepository _repo = Substitute.For<ICrepePartyRepository>();
        private readonly IValidator<UserDtoAdd> _addValidator = Substitute.For<IValidator<UserDtoAdd>>();
        private readonly IValidator<UserDtoUpdate> _updateValidator = Substitute.For<IValidator<UserDtoUpdate>>();

        private ChefManagementService CreateService()
            => new(_repo, _addValidator, _updateValidator);

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
                new User { Id = 1, FirstName = "John", LastName = "Doe" },
                new User { Id = 2, FirstName = "Jane", LastName = "Smith" }
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
            UserDto existing = new() { Id = 1, FirstName = "Old", LastName = "Name" };
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
            UserDto existing = new() { Id = 1, FirstName = "Old", LastName = "Name" };
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
                u.Id == 1 &&
                u.FirstName == "New" &&
                u.LastName == "Value"
            ));
        }


        [Fact]
        public async Task UpdateUserAsync_Should_Update_User_And_Return_Updated_Dto_With_Fixed_ID()
        {
            // Arrange
            UserDto existing = new() { Id = 1, FirstName = "Old", LastName = "Name" };
            UserDtoUpdate update = new() { Id=42, FirstName = "New", LastName = "Value" };

            _updateValidator.Validate(update).Returns(new ValidationResult());

            var service = CreateService();

            // Act
            var result = await service.UpdateUserAsync(existing, update);

            // Assert
            result.Should().NotBeNull();
            result!.FirstName.Should().Be("New");
            result.LastName.Should().Be("Value");
            result.Id.Should().Be(1);

            await _repo.Received(1).UpdateChefAsync(Arg.Is<User>(u =>
                u.Id == 1 &&
                u.FirstName == "New" &&
                u.LastName == "Value"
            ));
        }
    }
}
