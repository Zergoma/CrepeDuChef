using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Localization.Resources;
using CrepeDuChef.Application.Validation;
using CrepeDuChef.Tests.Helpers;
using FluentAssertions;

namespace CrepeDuChef.Tests.Application.Validation
{
    public class UserDtoUpdateValidatorTests
    {
        private readonly UserDtoUpdateValidator _validator;

        public UserDtoUpdateValidatorTests()
        {
            _validator = new UserDtoUpdateValidator(new FakeLocalizer<ValidationResources>());
        }

        [Fact]
        public void Should_Fail_When_Id_Is_Non_Init()
        {
            // Arrange
            UserDtoUpdate dto = new()
            {
                Id = 0,
                FirstName = "John",
                LastName = "Doe"
            };

            // Act
            var result = _validator.Validate(dto);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.Id));
        }


        [Fact]
        public void Should_Pass_When_Data_Is_Valid()
        {
            // Arrange
            UserDtoUpdate dto = new()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };

            // Act
            var result = _validator.Validate(dto);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Should_Fail_When_FirstName_Is_Empty_Or_Whitespace(string invalidValue)
        {
            // Arrange
            UserDtoUpdate dto = new()
            {
                Id = 1,
                FirstName = invalidValue,
                LastName = "Doe"
            };

            // Act
            var result = _validator.Validate(dto);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.FirstName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Should_Fail_When_LastName_Is_Empty_Or_Whitespace(string invalidValue)
        {
            // Arrange
            UserDtoUpdate dto = new()
            {
                Id = 1,
                FirstName = "John",
                LastName = invalidValue
            };

            // Act
            var result = _validator.Validate(dto);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.LastName));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("\t", "\n")]
        public void Should_Fail_When_FirstName_And_LastName_Are_Invalid(string invalidFirst, string invalidLast)
        {
            // Arrange
            UserDtoUpdate dto = new()
            {
                Id = 1,
                FirstName = invalidFirst,
                LastName = invalidLast
            };

            // Act
            var result = _validator.Validate(dto);

            // Assert
            result.IsValid.Should().BeFalse();

            result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.FirstName));
            result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.LastName));

            result.Errors.Should().HaveCount(2);
        }
    }
}