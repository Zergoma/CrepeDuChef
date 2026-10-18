using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Localization.Resources;
using CrepeDuChef.Application.Validation;
using CrepeDuChef.Tests.Helpers;
using FluentAssertions;

namespace CrepeDuChef.Tests.Application.Validation
{
    public class UserDtoAddValidatorTests
    {
        private readonly UserDtoAddValidator _validator;

        public UserDtoAddValidatorTests()
        {
            _validator = new UserDtoAddValidator(new FakeLocalizer<ValidationResources>());
        }

        [Fact]
        public void Should_Pass_When_Data_Is_Valid()
        {
            // Arrange
            UserDtoAdd dto = new()
            {
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
            UserDtoAdd dto = new()
            {
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
            UserDtoAdd dto = new()
            {
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
            UserDtoAdd dto = new()
            {
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

        [Fact]
        public void Should_Fail_With_Length_Message_When_FirstName_Is_Too_Long()
        {
            // Arrange
            UserDtoAdd dto = new()
            {
                FirstName = new string('a', User.NameMaxLength + 1),
                LastName = "Doe"
            };

            // Act
            var result = _validator.Validate(dto);

            // Assert
            result.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be("FirstNameLengthRange");
        }

        [Fact]
        public void Should_Fail_With_Length_Message_When_LastName_Is_Too_Long()
        {
            // Arrange
            UserDtoAdd dto = new()
            {
                FirstName = "John",
                LastName = new string('a', User.NameMaxLength + 1)
            };

            // Act
            var result = _validator.Validate(dto);

            // Assert
            result.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be("LastNameLengthRange");
        }
    }
}
