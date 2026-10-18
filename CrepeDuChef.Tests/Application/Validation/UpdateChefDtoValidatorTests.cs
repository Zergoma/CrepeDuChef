using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Localization.Resources;
using CrepeDuChef.Application.Validation;
using CrepeDuChef.Tests.Helpers;
using FluentAssertions;

namespace CrepeDuChef.Tests.Application.Validation
{
    public class UpdateChefDtoValidatorTests
    {
        private readonly UpdateChefDtoValidator _validator;

        public UpdateChefDtoValidatorTests()
        {
            _validator = new UpdateChefDtoValidator(new FakeLocalizer<ValidationResources>());
        }

        Guid IdTest = Guid.Parse("00000000-0000-0000-0000-000000000042");


        [Fact]
        public void Should_Pass_When_Data_Is_Valid()
        {
            // Arrange
            UpdateChefDto dto = new()
            {
                Id = IdTest,
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
            UpdateChefDto dto = new()
            {
                Id = IdTest,
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
            UpdateChefDto dto = new()
            {
                Id = IdTest,
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
            UpdateChefDto dto = new()
            {
                Id = IdTest,
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
            UpdateChefDto dto = new()
            {
                Id = IdTest,
                FirstName = new string('a', Chef.NameMaxLength + 1),
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
            UpdateChefDto dto = new()
            {
                Id = IdTest,
                FirstName = "John",
                LastName = new string('a', Chef.NameMaxLength + 1)
            };

            // Act
            var result = _validator.Validate(dto);

            // Assert
            result.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be("LastNameLengthRange");
        }
    }
}