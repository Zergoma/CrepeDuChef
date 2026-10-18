using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Domain.Entities;
using FluentAssertions;

namespace CrepeDuChef.Tests.Application.Mappers
{
    public class ChefMapperTests
    {
        private static readonly Guid IdTest = Guid.Parse("00000000-0000-0000-0000-000000000042");

        [Fact]
        public void ToDto_Should_Map_All_Fields_Correctly()
        {
            // Arrange
            Chef chef = new()
            {
                Id = IdTest,
                FirstName = "John",
                LastName = "Doe"
            };

            // Act
            ChefDto dto = chef.ToDto();

            // Assert
            dto.Id.Should().Be(IdTest);
            dto.FirstName.Should().Be("John");
            dto.LastName.Should().Be("Doe");
            dto.FullName.Should().Be("John Doe");
        }
    }
}
