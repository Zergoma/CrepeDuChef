using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Domain.Exceptions;
using FluentAssertions;

namespace CrepeDuChef.Tests.Domain.Entities
{
    public class ChefTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void FirstName_Should_Throw_When_Empty_Or_Whitespace(string invalid)
        {
            // Act
            Action act = () => new Chef { FirstName = invalid, LastName = "Doe" };

            // Assert
            act.Should().Throw<EmptyFirstNameException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void LastName_Should_Throw_When_Empty_Or_Whitespace(string invalid)
        {
            // Act
            Action act = () => new Chef { FirstName = "John", LastName = invalid };

            // Assert
            act.Should().Throw<EmptyLastNameException>();
        }

        [Fact]
        public void FullName_Should_Join_First_And_Last_Name()
        {
            // Arrange
            Chef chef = new() { FirstName = "John", LastName = "Doe" };

            // Assert
            chef.FullName.Should().Be("John Doe");
        }
    }
}
