using CrepeDuChef.Common;
using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Mappers;
using FluentAssertions;

namespace CrepeDuChef.Tests.Common.Mappers
{
    public class UserFormResultMapperTests
    {
        private readonly UserFormResultMapper _mapper = new();

        [Fact]
        public void Map_ShouldReturnCancelled_WhenResultsIsNull()
        {
            // Arrange
            List<string?>? toMap = null;

            // Act
            UserDataResult result = _mapper.Map(toMap);

            // Assert
            result.Status.Should().Be(FormResultStatus.Cancelled);
        }

        [Fact]
        public void Map_ShouldReturnInvalid_WhenListIsTooShort()
        {
            // Arrange
            List<string?> toMap = ["John"];

            // Act
            UserDataResult result = _mapper.Map(toMap);

            // Assert
            result.Status.Should().Be(FormResultStatus.InvalidDataForm);
        }

        [Fact]
        public void Map_ShouldReturnInvalid_WhenValuesAreEmpty()
        {
            // Arrange
            List<string?> toMap = ["", "Smith"];

            // Act
            UserDataResult result = _mapper.Map(toMap);

            // Assert
            result.Status.Should().Be(FormResultStatus.Invalid);
        }

        [Fact]
        public void Map_ShouldReturnSuccess_WhenValidData()
        {
            // Arrange
            List<string?> toMap = ["John", "Smith"];

            // Act
            UserDataResult result = _mapper.Map(toMap);

            // Assert
            result.Status.Should().Be(FormResultStatus.Success);
            result.FirstNameUpdate.Should().Be("John");
            result.LastNameUpdate.Should().Be("Smith");
        }
    }
}
