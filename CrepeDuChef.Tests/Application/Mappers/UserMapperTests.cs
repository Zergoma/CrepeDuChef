using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Extensions;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Domain.Exceptions;
using FluentAssertions;

namespace CrepeDuChef.Tests.Application.Mappers
{
    public class UserMapperTests
    {
        // ---------------------------
        // User → UserDto
        // ---------------------------

        [Fact]
        public void ToDto_Should_Map_All_Fields_Correctly()
        {
            // Arrange
            User user = new()
            {
                Id = 42,
                FirstName = "John",
                LastName = "Doe"
            };

            // Act
            UserDto dto = user.ToDto();

            // Assert
            dto.Id.Should().Be(42);
            dto.FirstName.Should().Be("John");
            dto.LastName.Should().Be("Doe");
        }

        // ---------------------------
        // UserDto → User
        // ---------------------------

        [Fact]
        public void ToEntity_Should_Map_All_Fields_Correctly()
        {
            // Arrange
            UserDto dto = new()
            {
                Id = 42,
                FirstName = "John",
                LastName = "Doe"
            };

            // Act
            User entity = dto.ToEntity();

            // Assert
            entity.Id.Should().Be(42);
            entity.FirstName.Should().Be("John");
            entity.LastName.Should().Be("Doe");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void ToEntity_Should_Throw_When_FirstName_Is_Invalid(string invalid)
        {
            // Arrange
            UserDto dto = new()
            {
                Id = 1,
                FirstName = invalid,
                LastName = "Doe"
            };

            // Act
            Action act = () => dto.ToEntity();

            // Assert
            act.Should().Throw<EmptyFirstNameException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void ToEntity_Should_Throw_When_LastName_Is_Invalid(string invalid)
        {
            // Arrange
            UserDto dto = new()
            {
                Id = 1,
                FirstName = "John",
                LastName = invalid
            };

            // Act
            Action act = () => dto.ToEntity();

            // Assert
            act.Should().Throw<EmptyLastNameException>();
        }
    }
}
